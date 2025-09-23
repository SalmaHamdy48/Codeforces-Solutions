using AutoMapper;
using MediatR;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Command.Handlers
{
    public class CreateCourseHandler(ICourseRepository courseRepository, IMapper mapper)
        : IRequestHandler<CreateCourseDto, Response>
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Response> Handle(CreateCourseDto request, CancellationToken cancellationToken)
        {
            var course = _mapper.Map<UniversitySystem.Models.Course>(request); 

            var existingCourse = await _courseRepository.GetByCodeAsync(request.Code, cancellationToken);
            if (existingCourse != null)
            {
                return Response.ErrorResponse(
                    $"Course code '{request.Code}' already exists. Please use a unique code.",
                    statusCode: HttpStatusCode.Conflict
                );
            }

            await _courseRepository.AddAsync(course, cancellationToken);

            return new Response
            {
                Data = course,
                Message = "Course added successfully",
                Status = true,
                StatusCode = HttpStatusCode.Created
            };
        }
    }
}