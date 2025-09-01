namespace CompanyApi.Models;


public class Project
{
    public int P_No { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }


// Has (owned by department)
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }


    public ICollection<WorksOnHours> WorksOn { get; set; } = new List<WorksOnHours>();
}