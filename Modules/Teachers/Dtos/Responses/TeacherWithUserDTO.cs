namespace SchoolManagementSystem.Teachers.Dtos.Responses;

public class TeacherWithUserDto
{
    public int UserId { get; set; } // dari User
    public string FullName { get; set; } = string.Empty; // dari User
    public string Role { get; set; }  = string.Empty; // dari User
    public DateTime CreatedAt { get; set; } // dari User

    public int TeacherId { get; set; } // dari Teacher
    public string TeacherNumber { get; set; } = string.Empty; // dari Teacher
    public string Gender { get; set; } = string.Empty; // dari Teacher
    public string Address { get; set; } = string.Empty; // dari Teacher
}