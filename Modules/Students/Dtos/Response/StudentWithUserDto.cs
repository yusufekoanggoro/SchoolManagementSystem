namespace SchoolManagementSystem.Students.Dtos.Responses;

public class StudentWithUserDto
{
    public int UserId { get; set; }         // dari User
    public string FullName { get; set; }    // dari User
    public string Role { get; set; }        // dari User
    public DateTime CreatedAt { get; set; } // dari User

    public int StudentId { get; set; }         // dari User
    public string StudentNumber { get; set; }   // dari Student
    public string Gender { get; set; }          // dari Student
    public string Address { get; set; }         // dari Student
}