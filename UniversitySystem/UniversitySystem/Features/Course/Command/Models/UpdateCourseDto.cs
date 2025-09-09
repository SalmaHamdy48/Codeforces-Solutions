namespace UniversitySystem.Features.Course.Command.Models;

public class UpdateCourseDto
{
    public int Id { get; set; }
    public string Cname { get; set; }
    public int Hours { get; set; }
}