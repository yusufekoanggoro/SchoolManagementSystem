namespace SchoolManagementSystem.Classes.Repositories.Interfaces;

using SchoolManagementSystem.Entities;

public interface IClassRepository
{
    Task<Class> AddClassAsync(Class classEntity);
    Task<Class?> GetClassByIdAsync(int classId);
    Task UpdateClassAsync(Class classEntity);
    Task<int> CountAllClassesAsync();
    Task<IEnumerable<Class>> GetClassesWithTeachersPagedAsync(int skip, int take, string sortBy, string sortDirection);
}