using AutoMapper;
using MediatR;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Query.Models;
using UniversitySystem.Global;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;
using System.Net;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Features.Student.Command.Models;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Query.Handlers
{
    public class GetCourseByIdHandler(ICourseRepository courseRepository, IMapper mapper)
        : IRequestHandler<GetCourseByIdQuery, Response>
    {
        public async Task<Response> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new CourseWithStudentsSpecification(request.Id);
            var course = await courseRepository.GetSingleAsync(spec, cancellationToken);

            if (course == null)
            {
                return Response.ErrorResponse(
                    $"Course with ID {request.Id} not found",
                    statusCode: HttpStatusCode.NotFound
                );
            }
            var courseData = mapper.Map<CourseDto>(course);

            var responseData = new
            {
                Course = courseData,
                Message = "Course retrieved successfully"
            };

            return Response.SuccessResponse(
                responseData,
                "Course retrieved successfully",
                HttpStatusCode.OK
            );
        }
    }
}