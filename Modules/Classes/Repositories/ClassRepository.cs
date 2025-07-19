namespace SchoolManagementSystem.Classes.Repositories;

using SchoolManagementSystem.Classes.Dtos.Requests;
using SchoolManagementSystem.Classes.Dtos.Responses;
using SchoolManagementSystem.Configuration;
using SchoolManagementSystem.Classes.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Entities;

public class ClassRepository : IClassRepository
{
    private readonly AppDbContext _context;

    public ClassRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Class> AddClassAsync(Class classEntity)
    {
        _context.Classes.Add(classEntity);
        await _context.SaveChangesAsync();
        return classEntity;
    }

    public async Task<Class?> GetClassByIdAsync(int classId)
    {
        return await _context.Classes
            .Include(c => c.Teacher) // Optional: kalau perlu ambil data teacher juga
            .FirstOrDefaultAsync(c => c.Id == classId);
    }

    public async Task UpdateClassAsync(Class classEntity)
    {
        _context.Classes.Update(classEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAllClassesAsync()
    {
        return await _context.Classes.CountAsync();
    }

    public async Task<IEnumerable<Class>> GetClassesWithTeachersPagedAsync(int skip, int take, string sortBy, string sortDirection)
    {
        IQueryable<Class> query = _context.Classes
            .Include(s => s.Teacher)
            .ThenInclude(t => t.User);

        switch (sortBy.ToLower())
        {
            case "name":
                query = sortDirection.ToLower() == "desc"
                    ? query.OrderByDescending(s => s.Name)
                    : query.OrderBy(s => s.Name);
                break;
            default:
                // Default sorting by FullName
                query = query.OrderBy(s => s.Id);
                break;
        }

        return await query
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}