namespace ApiTask.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}