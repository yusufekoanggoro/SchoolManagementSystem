using System.ComponentModel.DataAnnotations;

public class AssignTeacherDto
{
    [Required(ErrorMessage = "Nama kelas wajib diisi.")]
    [StringLength(100, ErrorMessage = "Nama kelas maksimal 100 karakter.")]
    public int TeacherId { get; set; }
}