
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Entities
{
    [Table("teachers")]
    public class Teacher
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("teacher_number")]
        [MaxLength(20)]
        public string TeacherNumber { get; set; }

        [Column("birth_date")]
        public DateTime? BirthDate { get; set; }

        [Column("gender")]
        [MaxLength(10)]
        public string? Gender { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        // Relasi ke Users (optional jika kamu pakai relasi navigasi)
        // public virtual User? User { get; set; }
        public User User { get; set; } = null!;

        // one to many
        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
