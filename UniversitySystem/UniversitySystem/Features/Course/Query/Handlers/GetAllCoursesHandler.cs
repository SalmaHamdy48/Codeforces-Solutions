using AutoMapper;
using MediatR;
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
    public class GetAllCoursesHandler(ICourseRepository courseRepository, IMapper mapper)
        : IRequestHandler<GetAllCoursesQuery, Response>
    {
        public async Task<Response> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var page = Math.Max(1, request.Page ?? 1);
            var pageSize = Math.Min(50, Math.Max(1, request.PageSize ?? 10));
            var skip = (page - 1) * pageSize;

            var spec = new AllCoursesSpecification(skip, pageSize);
            var courses = await courseRepository.GetListAsync(spec, cancellationToken);

            var totalCount = await courseRepository.CountAsync(new AllCoursesSpecification(), cancellationToken);

            var coursesData = mapper.Map<List<CourseDto>>(courses);

            var responseData = new
            {
                Courses = coursesData,
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
                $"Courses retrieved successfully. Page {page} of {(int)Math.Ceiling((double)totalCount / pageSize)}",
                HttpStatusCode.OK
            );
        }
    }
}