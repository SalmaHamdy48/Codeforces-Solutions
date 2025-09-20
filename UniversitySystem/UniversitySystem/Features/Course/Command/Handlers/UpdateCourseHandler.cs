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
    public class UpdateCourseHandler(ICourseRepository courseRepository, IMapper mapper)
        : IRequestHandler<UpdateCourseDto, Response>
    {
        public async Task<Response> Handle(UpdateCourseDto request, CancellationToken cancellationToken)
        {
            var spec = new CourseSpecification(request.Id);
            var existingCourse = await courseRepository.GetSingleAsync(spec, cancellationToken);

            if (existingCourse == null)
            {
                return Response.ErrorResponse(
                    $"Course with ID {request.Id} not found",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            var codeSpec = new CourseCodeExistsSpecification(request.Code, request.Id);
            var codeExists = await courseRepository.CountAsync(codeSpec, cancellationToken) > 0;

            if (codeExists)
            {
                return Response.ErrorResponse(
                    $"Course code '{request.Code}' already exists. Please use a unique code.",
                    statusCode: HttpStatusCode.Conflict
                );
            }

            mapper.Map(request, existingCourse);

            var updatedCourse = await courseRepository.UpdateAsync(existingCourse, cancellationToken);
            
            var responseData = new
            {
                Id = updatedCourse.Id,
                Code = updatedCourse.Code,
                Cname = updatedCourse.Cname,
                Hours = updatedCourse.Hours,
                Message = "Course updated successfully"
            };

            return Response.SuccessResponse(
                responseData,
                "Course updated successfully",
                HttpStatusCode.OK
            );
        }
    }
}