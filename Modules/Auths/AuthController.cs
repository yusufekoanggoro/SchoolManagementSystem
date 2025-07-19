namespace SchoolManagementSystem.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SchoolManagementSystem.Common.Response;
using SchoolManagementSystem.Auths.Dtos.Requests;
using SchoolManagementSystem.Auths.Services.Interfaces;

[ApiController]
[Route("api/login")]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "Validation error",
                Data = ModelState.ToString(),
            });
        }

        try
        {
            var isSuccess = await _authService.Login(dto);
            if (!isSuccess)
                return Unauthorized(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Username atau password salah.",
                });

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Login berhasil.",
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = "Internal server error",
                Data = ex.Message
            });
        }

    }

}