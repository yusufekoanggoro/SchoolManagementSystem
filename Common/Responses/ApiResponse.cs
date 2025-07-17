namespace SchoolManagementSystem.Common.Response;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public Meta Meta { get; set; }
}

public class Meta
{
    public PaginationMeta Pagination { get; set; }
}

public class PaginationMeta
{
    public int CurrentPage { get; set; }
    public int PerPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}