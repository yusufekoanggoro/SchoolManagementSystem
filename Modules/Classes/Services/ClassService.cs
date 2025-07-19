namespace SchoolManagementSystem.Classes.Services;

using SchoolManagementSystem.Classes.Services.Interfaces;
using SchoolManagementSystem.Classes.Repositories.Interfaces;
using SchoolManagementSystem.Teachers.Repositories.Interfaces;
using SchoolManagementSystem.Classes.Dtos.Requests;
using SchoolManagementSystem.Classes.Dtos.Responses;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Common.Response;
using SchoolManagementSystem.Common.Requests;

using AutoMapper;

public class ClassService : IClassService
{
    private readonly IClassRepository _classRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;

    public ClassService(
        IClassRepository classRepository,
        ITeacherRepository teacherRepository,
        IMapper mapper
    )
    {
        _classRepository = classRepository;
        _mapper = mapper;
        _teacherRepository = teacherRepository;
    }

    public async Task<CreateClassResponseDto> AddClassAsync(CreateClassDto dto)
    {
        var newClass = new Class
        {
            Name = dto.Name,
        };

        await _classRepository.AddClassAsync(newClass);

        var dtoResult = _mapper.Map<CreateClassResponseDto>(newClass);

        return dtoResult;
    }

    public async Task<ClassWithTeacherDto?> AssignTeacherAsync(int classId, int teacherId)
    {
        var classEntity = await _classRepository.GetClassByIdAsync(classId);
        if (classEntity == null) return null;

        var teacherEntity = await _teacherRepository.FindByIdAsync(teacherId);
        if (teacherEntity == null) return null;

        classEntity.TeacherId = teacherId;
        classEntity.Teacher = teacherEntity;

        await _classRepository.UpdateClassAsync(classEntity);

        var result = _mapper.Map<ClassWithTeacherDto>(classEntity);
        return result;
    }

    public async Task<ClassWithTeacherDto?> UnassignTeacherAsync(int classId)
    {
        var classEntity = await _classRepository.GetClassByIdAsync(classId);
        if (classEntity == null) return null;

        classEntity.TeacherId = null;
        await _classRepository.UpdateClassAsync(classEntity);

        var result = _mapper.Map<ClassWithTeacherDto>(classEntity);
        return result;
    }

    public async Task<(IEnumerable<ClassWithTeacherDto> Data, PaginationMeta Pagination)> GetClassesPagedAsync(PaginationDto request)
	{
		var skip = (request.Page - 1) * request.Size;
		var totalCount = await _classRepository.CountAllClassesAsync();
		var classes = await _classRepository.GetClassesWithTeachersPagedAsync(skip, request.Size, request.SortBy, request.SortDirection);
        
		var mapped = _mapper.Map<IEnumerable<ClassWithTeacherDto>>(classes);

		var pagination = new PaginationMeta
		{
			CurrentPage = request.Page,
			PerPage = request.Size,
			TotalPages = (int)Math.Ceiling(totalCount / (double)request.Size),
			TotalItems = totalCount
		};

		return (mapped, pagination);
	}

}