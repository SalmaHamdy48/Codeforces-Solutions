using AutoMapper;
using MediatR;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Command.Handlers
{
    public class CreateCourseHandler(ICourseRepository courseRepository, IMapper mapper)
        : IRequestHandler<CreateCourseDto, Response>
    {
        public async Task<Response> Handle(CreateCourseDto request, CancellationToken cancellationToken)
        {
            var course = mapper.Map<UniversitySystem.Models.Course>(request);
            
            var codeSpec = new CourseCodeExistsSpecification(request.Code);
            var codeExists = await courseRepository.CountAsync(codeSpec, cancellationToken) > 0;

            if (codeExists)
            {
                return Response.ErrorResponse(
                    $"Course code '{request.Code}' already exists. Please use a unique code.",
                    statusCode: HttpStatusCode.Conflict
                );
            }
            var createdCourse = await courseRepository.AddAsync(course, cancellationToken);
            
            var responseData = new
            {
                Id = createdCourse.Id,
                Code = createdCourse.Code,
                Cname = createdCourse.Cname,
                Hours = createdCourse.Hours,
                Message = "Course created successfully"
            };

            return Response.SuccessResponse(
                responseData,
                "Course created successfully",
                HttpStatusCode.Created
            );
        }
    }
}