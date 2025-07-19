namespace SchoolManagementSystem.Auths.Dtos.Requests;

using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required(ErrorMessage = "Username wajib diisi")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password wajib diisi")]
    public string Password { get; set; } = string.Empty;
}