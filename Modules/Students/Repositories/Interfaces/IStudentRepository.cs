namespace SchoolManagementSystem.Students.Repositories.Interfaces;
using SchoolManagementSystem.Entities;

public interface IStudentRepository
{
    Task<Student> AddStudentAsync(Student student);
    Task<Student?> FindStudentWithUserAsync(int studentId);
    Task<int> CountAllStudentsAsync();
    Task<IEnumerable<Student>> GetStudentsWithUsersPagedAsync(int skip, int take, string sortBy, string sortDirection);

    Task<Student?> FindByIdAsync(int id);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int studentId);
}