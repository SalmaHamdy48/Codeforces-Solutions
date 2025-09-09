using UniversitySystem.Models.Base;

namespace UniversitySystem.Models;

public class Course : BaseEntity
{
    public string Cname { get; set; }
    public int Hours { get; set; }
}