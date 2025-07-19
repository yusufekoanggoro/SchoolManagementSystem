namespace SchoolManagementSystem.Enrollments.Repositories.Interfaces;

using SchoolManagementSystem.Entities;

public interface IEnrollmentRepository
{
    Task<Enrollment?> CreateEnrollmentAsync(Enrollment enrollment);
    Task<IEnumerable<Enrollment>> GetEnrollmentsWithDetailsPagedAsync(int skip, int take, string sortBy, string sortDirection);

    Task<int> CountAllEnrollmentsAsync();
}
