namespace CompanyApi.Models;


public class WorksOnHours
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }


    public int ProjectId { get; set; }
    public Project? Project { get; set; }


    public int Hours { get; set; }
}