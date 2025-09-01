namespace CompanyApi.Models;


public class Manages
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }


    public int DepartmentId { get; set; }
    public Department? Department { get; set; }


    public DateOnly? Since { get; set; }
}