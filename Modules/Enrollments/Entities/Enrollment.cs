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
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;
    }
}
