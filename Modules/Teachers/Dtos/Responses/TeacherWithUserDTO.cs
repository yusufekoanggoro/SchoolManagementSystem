namespace SchoolManagementSystem.Teachers.Dtos.Responses;

public class TeacherWithUserDto
{
    public int UserId { get; set; }         // dari User
    public string FullName { get; set; }    // dari User
    public string Role { get; set; }        // dari User
    public DateTime CreatedAt { get; set; } // dari User

    public int TeacherId { get; set; }         // dari User
    public string TeacherNumber { get; set; }   // dari Student
    public string Gender { get; set; }          // dari Student
    public string Address { get; set; }         // dari Student
}