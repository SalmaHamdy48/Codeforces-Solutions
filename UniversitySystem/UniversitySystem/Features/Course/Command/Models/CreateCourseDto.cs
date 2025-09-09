namespace UniversitySystem.Features.Course.Command.Models;

public class CreateCourseDto
{
    public string Cname { get; set; } = string.Empty;
    public int Hours { get; set; }
}
