namespace SchoolManagementSystem.Classes.Services.Interfaces;
using SchoolManagementSystem.Classes.Dtos.Requests;
using SchoolManagementSystem.Classes.Dtos.Responses;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Response;

public interface IClassService
{
    Task<CreateClassResponseDto> AddClassAsync(CreateClassDto dto);
    Task<ClassWithTeacherDto?> AssignTeacherAsync(int classId, int teacherId);
    Task<ClassWithTeacherDto?> UnassignTeacherAsync(int classId);
    Task<(IEnumerable<ClassWithTeacherDto> Data, PaginationMeta Pagination)> GetClassesPagedAsync(PaginationDto request);
}
