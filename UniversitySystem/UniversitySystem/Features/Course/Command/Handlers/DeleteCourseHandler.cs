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
    public class DeleteCourseHandler(ICourseRepository courseRepository) : IRequestHandler<DeleteCourseDto, Response>
    {
        public async Task<Response> Handle(DeleteCourseDto request, CancellationToken cancellationToken)
        {
            
            var spec = new CourseWithStudentsSpecification(request.Id);
            var courseWithEnrollments = await courseRepository.GetSingleAsync(spec, cancellationToken);

            if (courseWithEnrollments == null)
            {
                return Response.ErrorResponse(
                    $"Course with ID {request.Id} not found",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            
            if (courseWithEnrollments.StudentCourses.Any())
            {
                var enrolledStudentsCount = courseWithEnrollments.StudentCourses.Count;
                return Response.ErrorResponse(
                    $"Cannot delete course with ID {request.Id}. It has {enrolledStudentsCount} enrolled student(s).",
                    statusCode: HttpStatusCode.BadRequest
                );
            }

            await courseRepository.DeleteAsync(courseWithEnrollments, cancellationToken);

            var responseData = new
            {
                Id = request.Id,
                Code = courseWithEnrollments.Code,
                Cname = courseWithEnrollments.Cname,
                Message = "Course deleted successfully"
            };

            return Response.SuccessResponse(
                responseData,
                "Course deleted successfully",
                HttpStatusCode.OK
            );
        }
    }
}