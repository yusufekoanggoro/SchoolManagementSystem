using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Enrollments.Dtos.Requests
{
    public class CreateEnrollmentDto
    {
        [Required(ErrorMessage = "StudentId wajib diisi.")]
        [Range(1, int.MaxValue, ErrorMessage = "StudentId harus lebih dari 0.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "ClassId wajib diisi.")]
        [Range(1, int.MaxValue, ErrorMessage = "ClassId harus lebih dari 0.")]
        public int ClassId { get; set; }
    }
}
