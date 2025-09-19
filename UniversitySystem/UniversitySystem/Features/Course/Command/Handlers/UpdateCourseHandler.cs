
using AutoMapper;
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
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseDto, Response>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCourseHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<Response> Handle(UpdateCourseDto request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCourse = await _context.Courses
                    .Include(c => c.StudentCourses)
                    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (existingCourse == null)
                {
                    return Response.ErrorResponse(
                        $"Course with ID {request.Id} not found",
                        statusCode: HttpStatusCode.NotFound
                    );
                }

                
                var codeExists = await _context.Courses
                    .AnyAsync(c => c.Id != request.Id && c.Code == request.Code, cancellationToken);

                if (codeExists)
                {
                    return Response.ErrorResponse(
                        $"Course code '{request.Code}' already exists",
                        statusCode: HttpStatusCode.Conflict
                    );
                }

                
                _mapper.Map(request, existingCourse);
                await _context.SaveChangesAsync(cancellationToken);

                var responseData = new
                {
                    Id = existingCourse.Id,
                    Code = existingCourse.Code,
                    Cname = existingCourse.Cname,
                    Hours = existingCourse.Hours,
                    Message = "Course updated successfully"
                };

                return Response.SuccessResponse(
                    responseData,
                    "Course updated successfully",
                    HttpStatusCode.OK
                );
            }
            catch (Exception ex)
            {
                return Response.ErrorResponse(
                    "Failed to update course",
                    new List<string> { ex.Message },
                    HttpStatusCode.InternalServerError
                );
            }
        }
    }
}