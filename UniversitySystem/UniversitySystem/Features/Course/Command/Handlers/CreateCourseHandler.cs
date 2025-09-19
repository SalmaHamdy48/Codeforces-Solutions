
using AutoMapper;
using MediatR;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Models;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Command.Handlers
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseDto, Response>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCourseHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateCourseDto request, CancellationToken cancellationToken)
        {
            try
            {
                var course = _mapper.Map<UniversitySystem.Models.Course>(request);
                _context.Courses.Add(course);
                await _context.SaveChangesAsync(cancellationToken);

                var responseData = new
                {
                    Id = course.Id,
                    Code = course.Code,
                    Cname = course.Cname,
                    Hours = course.Hours,
                    Message = "Course created successfully"
                };

                return Response.SuccessResponse(
                    responseData,
                    "Course created successfully",
                    HttpStatusCode.Created
                );
            }
            catch (Exception ex)
            {
                return Response.ErrorResponse(
                    "Failed to create course",
                    new List<string> { ex.Message },
                    HttpStatusCode.InternalServerError
                );
            }
        }
    }
}