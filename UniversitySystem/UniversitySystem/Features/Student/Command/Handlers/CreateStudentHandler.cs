
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
    public class CreateStudentHandler : IRequestHandler<CreateStudentDto, Response>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateStudentHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateStudentDto request, CancellationToken cancellationToken)
        {
            try
            {
                var student = _mapper.Map<UniversitySystem.Models.Student>(request);
                _context.Students.Add(student);
                await _context.SaveChangesAsync(cancellationToken);

                var responseData = new
                {
                    Id = student.Id,
                    Sname = student.Sname,
                    Age = student.Age,
                    Message = "Student created successfully"
                };

                return Response.SuccessResponse(
                    responseData,
                    "Student created successfully",
                    HttpStatusCode.Created
                );
            }
            catch (Exception ex)
            {
                return Response.ErrorResponse(
                    "Failed to create student",
                    new List<string> { ex.Message },
                    HttpStatusCode.InternalServerError
                );
            }
        }
    }
}