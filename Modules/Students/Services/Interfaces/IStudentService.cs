namespace SchoolManagementSystem.Students.Services.Interfaces;

using SchoolManagementSystem.Students.Dtos.Requests;
using SchoolManagementSystem.Students.Dtos.Responses;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Response;

public interface IStudentService
{
    Task<string> GenerateStudentNumberAsync();

    Task<StudentWithUserDto> AddStudentAsync(CreateStudentDto dto);

    Task<(IEnumerable<StudentWithUserDto> Data, PaginationMeta Pagination)> GetStudentsPagedAsync(PaginationDto request);

    Task<StudentWithUserDto> UpdateStudentAsync(UpdateStudentDto dto);

    Task<bool> DeleteStudentAsync(int studentId);
}