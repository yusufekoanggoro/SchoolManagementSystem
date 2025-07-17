namespace SchoolManagementSystem.Teachers.Services.Interfaces;

using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Response;
using SchoolManagementSystem.Teachers.Dtos.Requests;
using SchoolManagementSystem.Teachers.Dtos.Responses;

    public interface ITeacherService
    {
        Task<TeacherWithUserDto> AddTeacherAsync(CreateTeacherDto dto);

        Task<(IEnumerable<TeacherWithUserDto> Data, PaginationMeta Pagination)> GetTeachersPagedAsync(PaginationDto request);

        Task<TeacherWithUserDto> UpdateTeacherAsync(UpdateTeacherDto dto);

        Task<bool> DeleteTeacherAsync(int id);
    }
