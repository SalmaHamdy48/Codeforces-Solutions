using University.Models;
using UniversitySystem.Models.Base;

namespace UniversitySystem.Models;

public class Student : BaseEntity
{
    public string Sname { get; set; } = string.Empty;
    public int Age { get; set; }
    
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}