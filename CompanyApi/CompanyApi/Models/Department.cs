namespace CompanyApi.Models;


public class Department
{
    public int D_No { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }


    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Manages> Manages { get; set; } = new List<Manages>();
}