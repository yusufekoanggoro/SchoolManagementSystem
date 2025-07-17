namespace SchoolManagementSystem.Users.Repositories.Interfaces;

using SchoolManagementSystem.Entities;

    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<int> CountStudentsCreatedTodayAsync();
        Task<User?> FindByFullNameAsync(string fullName);

        Task<User?> FindByIdAsync(int id);

        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }
