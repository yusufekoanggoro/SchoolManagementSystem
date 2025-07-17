namespace SchoolManagementSystem.Students.Services;

using SchoolManagementSystem.Students.Dtos.Requests;
using SchoolManagementSystem.Students.Dtos.Responses;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Students.Repositories.Interfaces;
using SchoolManagementSystem.Users.Repositories.Interfaces;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Response;

using AutoMapper;
using SchoolManagementSystem.Students.Services.Interfaces;

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public StudentService(
            IStudentRepository studentRepository,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> GenerateStudentNumberAsync()
        {
            var today = DateTime.UtcNow.Date;
            var datePart = today.ToString("yyyyMMdd");

            var count = await _userRepository.CountStudentsCreatedTodayAsync();

            var number = (count + 1).ToString("D4"); // jadi 0001, 0002, dst

            return $"STD{datePart}{number}";
        }

        public async Task<StudentWithUserDto> AddStudentAsync(CreateStudentDto dto)
        {
            var existingUser = await _userRepository.FindByFullNameAsync(dto.FullName);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User dengan nama yang sama sudah terdaftar.");
            }

            var studentNumber = await GenerateStudentNumberAsync();

            var user = new User
            {
                FullName = dto.FullName,
                PasswordHash = dto.Password,
                Role = "student",
                CreatedAt = DateTime.UtcNow 
            };

            await _userRepository.AddUserAsync(user);

            var student = new Student
            {
                UserId = user.Id,
                StudentNumber = studentNumber,
                Gender = dto.Gender,
                Address = dto.Address,
            };

            await _studentRepository.AddStudentAsync(student);

            var studentWithUser = await _studentRepository.FindStudentWithUserAsync(student.Id);

            // Map ke DTO
            var dtoResult = _mapper.Map<StudentWithUserDto>(studentWithUser);

            return dtoResult;
        }

        public async Task<(IEnumerable<StudentWithUserDto> Data, PaginationMeta Pagination)> GetStudentsPagedAsync(PaginationDto request)
        {
			var skip = (request.Page - 1) * request.Size;
			var totalCount = await _studentRepository.CountAllStudentsAsync();
			var teachers = await _studentRepository.GetStudentsWithUsersPagedAsync(skip, request.Size, request.SortBy, request.SortDirection);

			var mapped = _mapper.Map<IEnumerable<StudentWithUserDto>>(teachers);

			var pagination = new PaginationMeta
			{
				CurrentPage = request.Page,
				PerPage = request.Size,
				TotalPages = (int)Math.Ceiling(totalCount / (double)request.Size),
				TotalItems = totalCount
			};

			return (mapped, pagination);
        }

        public async Task<StudentWithUserDto> UpdateStudentAsync(UpdateStudentDto dto)
        {
            var student = await _studentRepository.FindByIdAsync(dto.StudentId);
            if (student == null)
            {
                throw new InvalidOperationException("Student tidak ditemukan.");
            }

            var user = await _userRepository.FindByIdAsync(student.UserId);
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
            student.Gender = dto.Gender;
            student.Address = dto.Address;
            await _studentRepository.UpdateStudentAsync(student);

            var updated = await _studentRepository.FindStudentWithUserAsync(student.Id);
            return _mapper.Map<StudentWithUserDto>(updated);
        }

        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            var student = await _studentRepository.FindByIdAsync(studentId);
            if (student == null)
            {
                throw new InvalidOperationException("Student tidak ditemukan.");
            }

            var user = await _userRepository.FindByIdAsync(student.UserId);
            if (user == null)
            {
                throw new InvalidOperationException("User tidak ditemukan.");
            }

            // Hapus student terlebih dahulu karena memiliki foreign key ke user
            await _studentRepository.DeleteStudentAsync(student.Id);

            // Kemudian hapus user-nya
            await _userRepository.DeleteUserAsync(user.Id);

            return true;
        }

    }
