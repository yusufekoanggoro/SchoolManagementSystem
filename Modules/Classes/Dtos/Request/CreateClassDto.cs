namespace SchoolManagementSystem.Classes.Dtos.Requests;

using System.ComponentModel.DataAnnotations;

public class CreateClassDto
{
    [Required(ErrorMessage = "Nama kelas wajib diisi.")]
    [StringLength(100, ErrorMessage = "Nama kelas maksimal 100 karakter.")]
    public string Name { get; set; }

    public int? TeacherId { get; set; }
}
