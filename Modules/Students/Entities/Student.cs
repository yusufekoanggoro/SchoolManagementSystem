using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Entities
{
    [Table("students")]
    public class Student
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("student_number")]
        [MaxLength(20)]
        public string StudentNumber { get; set; }

        [Column("birth_date")]
        public DateTime? BirthDate { get; set; }

        [Column("gender")]
        [MaxLength(10)]
        public string? Gender { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        // Relasi ke Users (optional jika kamu pakai relasi navigasi)
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
