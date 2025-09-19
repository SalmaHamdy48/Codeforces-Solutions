
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Query.Models;
using UniversitySystem.Global;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Query.Handlers
{
    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, Response>
    {
        private readonly ApplicationDbContext _context;

        public GetAllCoursesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _context.Courses
                    .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                    .AsQueryable();

                
                var page = Math.Max(1, request.Page ?? 1);
                var pageSize = Math.Min(50, Math.Max(1, request.PageSize ?? 10));
                
                
                var totalCount = await _context.Courses.CountAsync(cancellationToken);
                
                
                var courses = await query
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
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
                    .ToListAsync(cancellationToken);

                var responseData = new
                {
                    Courses = courses,
                    Pagination = new
                    {
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalCount = totalCount,
                        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                        HasNextPage = page < Math.Ceiling((double)totalCount / pageSize),
                        HasPreviousPage = page > 1
                    }
                };

                return Response.SuccessResponse(
                    responseData,
                    $"Courses retrieved successfully. Page {page} of {(int)Math.Ceiling((double)totalCount / pageSize)}",
                    HttpStatusCode.OK
                );
            }
            catch (Exception ex)
            {
                return Response.ErrorResponse(
                    "Failed to retrieve courses",
                    new List<string> { ex.Message },
                    HttpStatusCode.InternalServerError
                );
            }
        }
    }
}