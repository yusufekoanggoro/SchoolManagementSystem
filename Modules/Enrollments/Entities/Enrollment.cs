using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Entities
{
    [Table("enrollments")]
    public class Enrollment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Student")]
        [Column("student_id")]
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        [ForeignKey("Class")]
        [Column("class_id")]
        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;
    }
}
