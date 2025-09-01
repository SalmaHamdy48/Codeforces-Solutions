namespace CompanyApi.Dto;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly? Dob { get; set; }
    public DateOnly? Doj { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public string? ImageUrl { get; set; }
}