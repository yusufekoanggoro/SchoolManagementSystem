namespace SchoolManagementSystem.Classes.Dtos.Responses;

public class ClassWithTeacherDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public TeacherDto Teacher { get; set; } = new(); 
}

public class TeacherDto
{
    public int Id { get; set; }
    public string TeacherNumber { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public UserDto User { get; set; } = new(); 
}

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}

