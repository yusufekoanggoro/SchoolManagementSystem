namespace SchoolManagementSystem.Enrollments.Services.Interfaces;

using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Response;
using SchoolManagementSystem.Enrollments.Dtos.Requests;
using SchoolManagementSystem.Enrollments.Dtos.Responses;

public interface IEnrollmentService
{
    Task<CreateEnrollmentResponseDto?> EnrollStudentAsync(CreateEnrollmentDto request);
    Task<(IEnumerable<EnrollmentWithStudentAndClassDto> Data, PaginationMeta Pagination)> GetEnrollmentsPagedAsync(PaginationDto request);
}
