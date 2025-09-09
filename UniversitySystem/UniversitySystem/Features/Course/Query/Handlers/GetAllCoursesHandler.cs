using MediatR;
using AutoMapper;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Features.Course.Query.Models;

namespace UniversitySystem.Features.Course.Query.Handlers
{
    public class GetAllCoursesHandler(ICourseRepository courseRepository, IMapper mapper)
        : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseListResponseDto>>
    {
        public async Task<IEnumerable<CourseListResponseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await courseRepository.GetAllAsync();
            return mapper.Map<IEnumerable<CourseListResponseDto>>(courses);
        }
    }
}