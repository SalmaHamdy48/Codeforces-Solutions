
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Features.Student.Query.Models;
using UniversitySystem.Global;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Query.Handlers
{
    public class GetStudentByIdHandler(ApplicationDbContext context) : IRequestHandler<GetStudentByIdQuery, Response>
    {
        public async Task<Response> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
                var student = await context.Students
                    .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                    .Where(s => s.Id == request.Id)
                    .Select(s => new
                    {
                        s.Id,
                        s.Sname,
                        s.Age,
                        EnrolledCourses = s.StudentCourses.Select(sc => new
                        {
                            sc.CourseId,
                            sc.Course.Code,
                            sc.Course.Cname,
                            sc.Course.Hours
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (student == null)
                {
                    return Response.ErrorResponse(
                        $"Student with ID {request.Id} not found",
                        statusCode: HttpStatusCode.NotFound
                    );
                }

                var responseData = new
                {
                    Student = student,
                    Message = "Student retrieved successfully"
                };

                return Response.SuccessResponse(
                    responseData, 
                    "Student retrieved successfully",
                    HttpStatusCode.OK
                );
        }
    }
}