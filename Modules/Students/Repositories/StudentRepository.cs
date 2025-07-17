namespace SchoolManagementSystem.Students.Repositories;

using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Configuration;
using SchoolManagementSystem.Students.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Student> AddStudentAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student?> FindStudentWithUserAsync(int studentId)
    {
        return await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == studentId);
    }

    public async Task<int> CountAllStudentsAsync()
    {
        return await _context.Students.CountAsync();
    }

    public async Task<IEnumerable<Student>> GetStudentsWithUsersPagedAsync(int skip, int take, string sortBy, string sortDirection)
    {
        IQueryable<Student> query = _context.Students.Include(s => s.User);

        switch (sortBy.ToLower())
            {
                case "fullname":
                    Console.WriteLine("asd");
                    query = sortDirection.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.User.FullName)
                        : query.OrderBy(s => s.User.FullName);
                    break;

                case "createdat":
                    query = sortDirection.ToLower() == "desc"
                        ? query.OrderByDescending(s => s.User.CreatedAt)
                        : query.OrderBy(s => s.User.CreatedAt);
                    break;

                default:
                    // Default sorting by FullName
                    query = query.OrderBy(s => s.User.FullName);
                    break;
            }

            return await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();

        }

        public async Task<Student?> FindByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

    }