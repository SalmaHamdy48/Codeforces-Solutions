
using AutoMapper;
using MediatR;
using UniversitySystem.Data;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Models;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Command.Handlers
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentDto, Response>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateStudentHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateStudentDto request, CancellationToken cancellationToken)
        {
            try
            {
                var existingStudent = await _context.Students.FindAsync(request.Id);
                if (existingStudent == null)
                {
                    return Response.ErrorResponse(
                        $"Student with ID {request.Id} not found",
                        statusCode: HttpStatusCode.NotFound
                    );
                }

                _mapper.Map(request, existingStudent);
                await _context.SaveChangesAsync(cancellationToken);

                var responseData = new
                {
                    Id = existingStudent.Id,
                    Sname = existingStudent.Sname,
                    Age = existingStudent.Age,
                    Message = "Student updated successfully"
                };

                return Response.SuccessResponse(
                    responseData,
                    "Student updated successfully",
                    HttpStatusCode.OK
                );
            }
            catch (Exception ex)
            {
                return Response.ErrorResponse(
                    "Failed to update student",
                    new List<string> { ex.Message },
                    HttpStatusCode.InternalServerError
                );
            }
        }
    }
}