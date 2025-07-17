namespace SchoolManagementSystem.Students.Dtos.Requests;

using System.ComponentModel.DataAnnotations;

public class CreateStudentDto
{
    [Required(ErrorMessage = "Nama lengkap wajib diisi.")]
    [StringLength(100, ErrorMessage = "Nama lengkap maksimal 100 karakter.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Password wajib diisi.")]
    [MinLength(6, ErrorMessage = "Password minimal 6 karakter.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Role wajib diisi.")]
    public string Role { get; set; }

    [Required(ErrorMessage = "Tanggal lahir wajib diisi.")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Jenis kelamin wajib diisi.")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Alamat wajib diisi.")]
    [StringLength(255, ErrorMessage = "Alamat maksimal 255 karakter.")]
    public string Address { get; set; }
}