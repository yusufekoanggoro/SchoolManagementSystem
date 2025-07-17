namespace SchoolManagementSystem.Students.Dtos.Requests;

using System.ComponentModel.DataAnnotations;

public class UpdateStudentDto
{
    [Required(ErrorMessage = "ID student wajib diisi.")]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "Nama lengkap wajib diisi.")]
    [StringLength(100, ErrorMessage = "Nama lengkap maksimal 100 karakter.")]
    public string FullName { get; set; }

    [MinLength(6, ErrorMessage = "Password minimal 6 karakter.")]
    public string? Password { get; set; } // optional, tapi divalidasi kalau diisi

    [Required(ErrorMessage = "Jenis kelamin wajib diisi.")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "Alamat wajib diisi.")]
    [StringLength(255, ErrorMessage = "Alamat maksimal 255 karakter.")]
    public string Address { get; set; }
}