namespace UniversitySystem.Features.Course.Query.Models
{
    public class CourseResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
    }
}