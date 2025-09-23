using AutoMapper;
using MediatR;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Command.Handlers
{
    public class CreateStudentHandler(IStudentRepository studentRepository, IMapper mapper)
        : IRequestHandler<CreateStudentDto, Response>
    {
        public async Task<Response> Handle(CreateStudentDto request, CancellationToken cancellationToken = default)
        {
            var student = mapper.Map<UniversitySystem.Models.Student>(request);
            var createdStudent = await studentRepository.AddAsync(student, cancellationToken);

            var responseData = mapper.Map<object>(createdStudent);

            return Response.SuccessResponse(
                responseData,
                "Student created successfully"
            );
        }
    }
}