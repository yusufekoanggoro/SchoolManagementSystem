namespace SchoolManagementSystem.Auths.Services.Interfaces;

using SchoolManagementSystem.Auths.Dtos.Requests;

public interface IAuthService
{
    Task<bool> Login(LoginDto dto);
};
