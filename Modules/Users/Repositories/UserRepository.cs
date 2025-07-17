namespace SchoolManagementSystem.Users.Repositories;

using SchoolManagementSystem.Entities;
using SchoolManagementSystem.Configuration;
using SchoolManagementSystem.Users.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<int> CountStudentsCreatedTodayAsync()
        {
            var today = DateTime.UtcNow.Date;

            return await _context.Users
                .Where(u => u.Role == "student" && u.CreatedAt.Date == today)
                .CountAsync();
        }

        public async Task<User?> FindByFullNameAsync(string fullName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.FullName == fullName);
        }

        public async Task<User?> FindByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

    }
