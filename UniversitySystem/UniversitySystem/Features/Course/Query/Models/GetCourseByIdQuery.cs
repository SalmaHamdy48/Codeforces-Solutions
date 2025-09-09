using MediatR;
using UniversitySystem.Features.Course.Query.Models;

namespace UniversitySystem.Features.Course.Query.Models
{
    public class GetCourseByIdQuery : IRequest<CourseResponseDto>
    {
        public int Id { get; set; }
    }
}