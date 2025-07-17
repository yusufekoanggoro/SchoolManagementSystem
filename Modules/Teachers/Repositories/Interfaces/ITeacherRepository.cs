namespace SchoolManagementSystem.Teachers.Repositories.Interfaces;

using SchoolManagementSystem.Entities;

    public interface ITeacherRepository
    {
        Task<Teacher> AddTeacherAsync(Teacher teacher);
        Task<Teacher?> FindTeacherWithUserAsync(int teacherId);
        Task<int> CountAllTeachersAsync();
        Task<IEnumerable<Teacher>> GetTeachersWithUsersPagedAsync(int skip, int take, string sortBy, string sortDirection);

        Task<Teacher?> FindByIdAsync(int id);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int teacherId);
    }
