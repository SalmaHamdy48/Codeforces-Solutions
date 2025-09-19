
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Models;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Command.Handlers
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseDto, Response>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCourseHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(DeleteCourseDto request, CancellationToken cancellationToken)
        {
            try
            {
                
                var courseWithEnrollments = await _context.Courses
                    .Include(c => c.StudentCourses)
                    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

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

                
                _context.Courses.Remove(courseWithEnrollments);
                await _context.SaveChangesAsync(cancellationToken);

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
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("FOREIGN KEY") == true)
            {
                return Response.ErrorResponse(
                    "Cannot delete course. It has active enrollments that must be removed first.",
                    new List<string> { ex.InnerException.Message },
                    HttpStatusCode.BadRequest
                );
            }
            catch (Exception ex)
            {
                return Response.ErrorResponse(
                    "Failed to delete course",
                    new List<string> { ex.Message },
                    HttpStatusCode.InternalServerError
                );
            }
        }
    }
}