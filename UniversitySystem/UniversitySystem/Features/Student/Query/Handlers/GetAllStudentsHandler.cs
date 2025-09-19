// Features/Student/Query/Handlers/GetAllStudentsHandler.cs
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Features.Student.Query.Models;
using UniversitySystem.Global;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Query.Handlers
{
    public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, Response>
    {
        private readonly ApplicationDbContext _context;

        public GetAllStudentsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _context.Students
                    .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                    .AsQueryable();

                var page = Math.Max(1, request.Page ?? 1);
                var pageSize = Math.Min(50, Math.Max(1, request.PageSize ?? 10));
                
                var students = await query
                    .OrderBy(s => s.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
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
                    .ToListAsync(cancellationToken);

                var totalCount = await _context.Students.CountAsync(cancellationToken);
                
                var responseData = new
                {
                    Students = students,
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
                    $"Students retrieved successfully. Page {page} of {(int)Math.Ceiling((double)totalCount / pageSize)}",
                    HttpStatusCode.OK
                );
            }
            catch (Exception ex)
            {
                return Response.ErrorResponse(
                    "Failed to retrieve students",
                    new List<string> { ex.Message },
                    HttpStatusCode.InternalServerError
                );
            }
        }
    }
}