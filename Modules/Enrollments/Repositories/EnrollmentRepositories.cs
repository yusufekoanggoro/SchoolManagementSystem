namespace SchoolManagementSystem.Enrollments.Repositories;

using SchoolManagementSystem.Enrollments.Repositories.Interfaces;
using SchoolManagementSystem.Configuration;
using SchoolManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;

    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollment?> CreateEnrollmentAsync(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsWithDetailsPagedAsync(int skip, int take, string sortBy, string sortDirection)
    {
        IQueryable<Enrollment> query = _context.Enrollments.Include(e => e.Student)
            .ThenInclude(s => s.User)
            .Include(e => e.Class);

        switch (sortBy.ToLower())
        {
            case "fullname":
                query = sortDirection.ToLower() == "desc"
                    ? query.OrderByDescending(s => s.Student.User.FullName)
                    : query.OrderBy(s => s.Student.User.FullName);
                break;

            default:
                // Default sorting by FullName
                query = query.OrderBy(s => s.Student.User.FullName);
                break;
        }
        ;

        return await query
            .Skip(skip)
            .Take(take)
            .ToListAsync();

    }
    
    public async Task<int> CountAllEnrollmentsAsync()
    {
        return await _context.Enrollments.CountAsync();
    }

    
}