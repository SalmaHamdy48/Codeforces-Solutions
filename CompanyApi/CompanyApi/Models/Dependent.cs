namespace CompanyApi.Models;


public class Dependent
{
    public int Id { get; set; }
    public string D_Name { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string Relationship { get; set; } = string.Empty;


    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}