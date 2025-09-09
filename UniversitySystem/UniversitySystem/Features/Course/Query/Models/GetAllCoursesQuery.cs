using MediatR;
using System.Collections.Generic;

namespace UniversitySystem.Features.Course.Query.Models
{
    public class GetAllCoursesQuery : IRequest<IEnumerable<CourseListResponseDto>>
    {
    }
}