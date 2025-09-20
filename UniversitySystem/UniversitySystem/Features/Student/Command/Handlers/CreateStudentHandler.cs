using AutoMapper;
using MediatR;
using UniversitySystem.Data;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Command.Handlers
{
    public class CreateStudentHandler(IStudentRepository studentRepository, IMapper mapper)
        : IRequestHandler<CreateStudentDto, Response>
    {
        public async Task<Response> Handle(CreateStudentDto request, CancellationToken cancellationToken = default)
        {
            var student = mapper.Map<UniversitySystem.Models.Student>(request);
            var createdStudent =
                await studentRepository.AddAsync(student, cancellationToken);

            var responseData = new
            {
                Id = createdStudent.Id,
                Sname = createdStudent.Sname,
                Age = createdStudent.Age,
                Message = "Student created successfully"
            };

            return Response.SuccessResponse(
                responseData,
                "Student created successfully"
            );
        }
    }
}