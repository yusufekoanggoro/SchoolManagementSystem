namespace SchoolManagementSystem.Enrollments.Dtos.Responses;

public class EnrollmentWithStudentAndClassDto
{
    public int EnrollmentId { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
}
