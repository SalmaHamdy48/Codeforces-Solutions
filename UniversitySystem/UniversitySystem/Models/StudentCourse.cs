using UniversitySystem.Models;
using UniversitySystem.Models.Base;

namespace  University.Models;

public class StudentCourse : BaseEntity
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
        
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    
    

}