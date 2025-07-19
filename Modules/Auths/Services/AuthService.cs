namespace SchoolManagementSystem.Auths.Services;

using SchoolManagementSystem.Auths.Services.Interfaces;
using SchoolManagementSystem.Auths.Dtos.Requests;
using SchoolManagementSystem.Users.Repositories.Interfaces;
using AutoMapper;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public AuthService(
        IUserRepository userRepository,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> Login(LoginDto dto)
    {
        var user = await _userRepository.FindByFullNameAsync(dto.Username);

        if (user == null) return false;

        if (user.PasswordHash != dto.Password) return false;

        return true;
    }

}