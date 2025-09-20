using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Features.Student.Query.Models;
using UniversitySystem.Global;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;
using System.Net;
using Response = UniversitySystem.Global.Response;
using AutoMapper;

namespace UniversitySystem.Features.Student.Query.Handlers
{
    public class GetAllStudentsHandler(IStudentRepository studentRepository)
        : IRequestHandler<GetAllStudentsQuery, Response>
    {
        public async Task<Response> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var page = Math.Max(1, request.Page ?? 1);
            var pageSize = Math.Min(50, Math.Max(1, request.PageSize ?? 10));
            var skip = (page - 1) * pageSize;

            var spec = new AllStudentsSpecification(skip, pageSize);
            var students = await studentRepository.GetListAsync(spec, cancellationToken);

            var countSpec = new AllStudentsSpecification();
            var totalCount = await studentRepository.CountAsync(countSpec, cancellationToken);

            var responseData = new
            {
                Students = students.Select(s => new
                {
                    s.Id,
                    s.Sname,
                    s.Age,
                    EnrolledCoursesCount = s.StudentCourses?.Count ?? 0,
                    EnrolledCourses = s.StudentCourses?.Select(sc => new
                    {
                        sc.CourseId,
                        sc.Course?.Code,
                        sc.Course?.Cname,
                        sc.Course?.Hours
                    }) ?? Enumerable.Empty<object>()
                }),
                Pagination = new
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = totalCount > 0 ? (int)Math.Ceiling((double)totalCount / pageSize) : 0,
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
    }
}
