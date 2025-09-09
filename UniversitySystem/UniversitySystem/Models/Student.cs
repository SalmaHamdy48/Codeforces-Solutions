using UniversitySystem.Models.Base;

namespace UniversitySystem.Models;

public class Student : BaseEntity
{
    public string Sname { get; set; }
    public int Age { get; set; }
}