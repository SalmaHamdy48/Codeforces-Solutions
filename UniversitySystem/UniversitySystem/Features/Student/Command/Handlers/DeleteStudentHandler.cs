using MediatR;
using UniversitySystem.Data;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Global;
using UniversitySystem.Models;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Specifications;
using System.Net;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Command.Handlers
{
    public class DeleteStudentHandler(IStudentRepository studentRepository)
        : IRequestHandler<DeleteStudentDto, Response>
    {
        public async Task<Response> Handle(DeleteStudentDto request, CancellationToken cancellationToken)
        {
            var spec = new StudentSpecification(request.Id);
            var student = await studentRepository.GetSingleAsync(spec, cancellationToken);

            if (student == null)
            {
                return Response.ErrorResponse(
                    $"Student with ID {request.Id} not found",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            await studentRepository.DeleteAsync(student, cancellationToken);

            var responseData = new
            {
                Id = request.Id,
                Message = "Student deleted successfully"
            };

            return Response.SuccessResponse(
                responseData,
                "Student deleted successfully",
                HttpStatusCode.OK
            );
        }
    }
}