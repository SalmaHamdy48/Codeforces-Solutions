
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Query.Models;
using UniversitySystem.Global;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Query.Handlers
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Response>
    {
        private readonly ApplicationDbContext _context;

        public GetCourseByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _context.Courses
                    .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                    .Where(c => c.Id == request.Id)
                    .Select(c => new
                    {
                        c.Id,
                        c.Code,
                        c.Cname,
                        c.Hours,
                        EnrolledStudentsCount = c.StudentCourses.Count,
                        EnrolledStudents = c.StudentCourses.Select(sc => new
                        {
                            sc.StudentId,
                            sc.Student.Sname,
                            sc.Student.Age
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (course == null)
                {
                    return Response.ErrorResponse(
                        $"Course with ID {request.Id} not found",
                        statusCode: HttpStatusCode.NotFound
                    );
                }

                var responseData = new
                {
                    Course = course,
                    Message = "Course retrieved successfully"
                };

                return Response.SuccessResponse(
                    responseData,
                    "Course retrieved successfully",
                    HttpStatusCode.OK
                );
            }
            catch (Exception ex)
            {
                return Response.ErrorResponse(
                    "Failed to retrieve course",
                    new List<string> { ex.Message },
                    HttpStatusCode.InternalServerError
                );
            }
        }
    }
}