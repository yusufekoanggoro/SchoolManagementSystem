namespace SchoolManagementSystem.Teachers.Repositories;

using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Configuration;
using SchoolManagementSystem.Teachers.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;

        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher> AddTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teacher?> FindTeacherWithUserAsync(int teacherId)
        {
            return await _context.Teachers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == teacherId);
        }

        public async Task<int> CountAllTeachersAsync()
        {
            return await _context.Teachers.CountAsync();
        }

        public async Task<IEnumerable<Teacher>> GetTeachersWithUsersPagedAsync(int skip, int take, string sortBy, string sortDirection)
        {
            IQueryable<Teacher> query = _context.Teachers.Include(s => s.User);

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

        public async Task<Teacher?> FindByIdAsync(int id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeacherAsync(int teacherId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        }

    }