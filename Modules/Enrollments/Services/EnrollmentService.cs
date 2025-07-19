namespace SchoolManagementSystem.Enrollments.Services;

using SchoolManagementSystem.Enrollments.Services.Interfaces;
using SchoolManagementSystem.Enrollments.Dtos.Requests;
using SchoolManagementSystem.Enrollments.Dtos.Responses;
using SchoolManagementSystem.Enrollments.Repositories.Interfaces;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Response;

using AutoMapper;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IMapper _mapper;

    public EnrollmentService(
        IEnrollmentRepository enrollmentRepository,
        IMapper mapper
    )
    {
        _enrollmentRepository = enrollmentRepository;
        _mapper = mapper;
    }

    public async Task<CreateEnrollmentResponseDto?> EnrollStudentAsync(CreateEnrollmentDto request)
    {
        var enrollment = new Enrollment
        {
            StudentId = request.StudentId,
            ClassId = request.ClassId
        };

        var created = await _enrollmentRepository.CreateEnrollmentAsync(enrollment);
        if (created == null) return null;

        return _mapper.Map<CreateEnrollmentResponseDto>(created);
    }

    public async Task<(IEnumerable<EnrollmentWithStudentAndClassDto> Data, PaginationMeta Pagination)> GetEnrollmentsPagedAsync(PaginationDto request)
	{
			var skip = (request.Page - 1) * request.Size;
			var totalCount = await _enrollmentRepository.CountAllEnrollmentsAsync();
			var enrollments = await _enrollmentRepository.GetEnrollmentsWithDetailsPagedAsync(skip, request.Size, request.SortBy, request.SortDirection);

			var mapped = _mapper.Map<IEnumerable<EnrollmentWithStudentAndClassDto>>(enrollments);

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