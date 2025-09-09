using MediatR;
using AutoMapper;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Features.Course.Query.Models;

namespace UniversitySystem.Features.Course.Query.Handlers
{
    public class GetCourseByIdHandler(ICourseRepository courseRepository, IMapper mapper)
        : IRequestHandler<GetCourseByIdQuery, CourseResponseDto>
    {
        public async Task<CourseResponseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetByIdAsync(request.Id);
            return course != null ? mapper.Map<CourseResponseDto>(course) : null;
        }
    }
}