namespace ApiTask.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}