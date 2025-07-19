namespace SchoolManagementSystem.Classes.Dtos.Responses;

public class CreateClassResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int? TeacherId { get; set; }
}
