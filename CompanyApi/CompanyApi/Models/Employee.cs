namespace CompanyApi.Models;


public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public DateOnly? Dob { get; set; }
    public DateOnly? Doj { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }


// Works_in
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }


// Image file name stored in DB
    public string? ImageFileName { get; set; }


    public ICollection<WorksOnHours> WorksOn { get; set; } = new List<WorksOnHours>();
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
    public ICollection<Manages> Manages { get; set; } = new List<Manages>();
}