namespace SchoolManagementSystem.Common.Requests;

public class PaginationDto
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? SortBy { get; set; } = "id";
    public string SortDirection { get; set; } = "asc";
}