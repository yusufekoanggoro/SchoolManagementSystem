namespace SchoolManagementSystem.Teachers.Services;

using SchoolManagementSystem.Teachers.Dtos.Requests;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Teachers.Repositories.Interfaces;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Response;
using SchoolManagementSystem.Teachers.Dtos.Responses;
using SchoolManagementSystem.Users.Repositories.Interfaces;
using SchoolManagementSystem.Teachers.Services.Interfaces;

using AutoMapper;

	public class TeacherService : ITeacherService
	{
		private readonly ITeacherRepository _teacherRepository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public TeacherService(
			ITeacherRepository teacherRepository,
			IUserRepository userRepository,
			IMapper mapper
		)
		{
			_teacherRepository = teacherRepository;
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<string> GenerateTeacherNumberAsync()
		{
			var today = DateTime.UtcNow.Date;
			var datePart = today.ToString("yyyyMMdd");

			var count = await _userRepository.CountStudentsCreatedTodayAsync();

			var number = (count + 1).ToString("D4"); // jadi 0001, 0002, dst

			return $"EMP{datePart}{number}";
		}

		public async Task<TeacherWithUserDto> AddTeacherAsync(CreateTeacherDto dto)
		{
			var existingUser = await _userRepository.FindByFullNameAsync(dto.FullName);
			if (existingUser != null)
			{
				throw new InvalidOperationException("User dengan nama yang sama sudah terdaftar.");
			}

			var teacherNumber = await GenerateTeacherNumberAsync();

			var user = new User
			{
				FullName = dto.FullName,
				PasswordHash = dto.Password,
				Role = "teacher",
				CreatedAt = DateTime.UtcNow
			};

			await _userRepository.AddUserAsync(user);

			var teacher = new Teacher
			{
				UserId = user.Id,
				TeacherNumber = teacherNumber,
				Gender = dto.Gender,
				Address = dto.Address,
			};

			await _teacherRepository.AddTeacherAsync(teacher);

			var teacherWithUser = await _teacherRepository.FindTeacherWithUserAsync(teacher.Id);

			// Map ke DTO
			var dtoResult = _mapper.Map<TeacherWithUserDto>(teacherWithUser);

			return dtoResult;
		}

		// public async Task<PaginatedResultDto<TeacherWithUserDto>> GetTeachersPagedAsync(PaginationRequestDto request)

		public async Task<(IEnumerable<TeacherWithUserDto> Data, PaginationMeta Pagination)> GetTeachersPagedAsync(PaginationDto request)
		{
			var skip = (request.Page - 1) * request.Size;
			var totalCount = await _teacherRepository.CountAllTeachersAsync();
			var teachers = await _teacherRepository.GetTeachersWithUsersPagedAsync(skip, request.Size, request.SortBy, request.SortDirection);

			var mapped = _mapper.Map<IEnumerable<TeacherWithUserDto>>(teachers);

			var pagination = new PaginationMeta
			{
				CurrentPage = request.Page,
				PerPage = request.Size,
				TotalPages = (int)Math.Ceiling(totalCount / (double)request.Size),
				TotalItems = totalCount
			};

			return (mapped, pagination);
		}

		public async Task<TeacherWithUserDto> UpdateTeacherAsync(UpdateTeacherDto dto)
		{
			var teacher = await _teacherRepository.FindByIdAsync(dto.TeacherId);
			if (teacher == null)
			{
				throw new InvalidOperationException("Student tidak ditemukan.");
			}

			var user = await _userRepository.FindByIdAsync(teacher.UserId);
			if (user == null)
			{
				throw new InvalidOperationException("User tidak ditemukan.");
			}

			// Update data user
			user.FullName = dto.FullName;
			if (!string.IsNullOrEmpty(dto.Password))
			{
				user.PasswordHash = dto.Password; // sebaiknya hash jika ada hashing logic
			}
			await _userRepository.UpdateUserAsync(user);

			// Update data student
			teacher.Gender = dto.Gender;
			teacher.Address = dto.Address;
			await _teacherRepository.UpdateTeacherAsync(teacher);

			var updated = await _teacherRepository.FindTeacherWithUserAsync(teacher.Id);
			return _mapper.Map<TeacherWithUserDto>(updated);
		}

        public async Task<bool> DeleteTeacherAsync(int teacherId)
        {
            var teacher = await _teacherRepository.FindByIdAsync(teacherId);
            if (teacher == null)
            {
                throw new InvalidOperationException("Teacher tidak ditemukan.");
            }

            var user = await _userRepository.FindByIdAsync(teacher.UserId);
            if (user == null)
            {
                throw new InvalidOperationException("User tidak ditemukan.");
            }

            // Hapus student terlebih dahulu karena memiliki foreign key ke user
            await _teacherRepository.DeleteTeacherAsync(teacher.Id);

            // Kemudian hapus user-nya
            await _userRepository.DeleteUserAsync(user.Id);

            return true;
        }

	}