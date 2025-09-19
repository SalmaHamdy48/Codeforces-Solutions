using University.Models;
using UniversitySystem.Models.Base;

namespace UniversitySystem.Models;

public class Course : BaseEntity
{
   public string Code { get; set; } = string.Empty;
    public string Cname { get; set; } = string.Empty;
    public int Hours { get; set; }
    
    //navigation property
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}