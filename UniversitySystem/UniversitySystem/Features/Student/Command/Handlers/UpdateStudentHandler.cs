using AutoMapper;
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
    public class UpdateStudentHandler(IStudentRepository studentRepository, IMapper mapper)
        : IRequestHandler<UpdateStudentDto, Response>
    {
        public async Task<Response> Handle(UpdateStudentDto request, CancellationToken cancellationToken)
        {
            var spec = new StudentSpecification(request.Id);
            var existingStudent = await studentRepository.GetSingleAsync(spec, cancellationToken);

            if (existingStudent == null)
            {
                return Response.ErrorResponse(
                    $"Student with ID {request.Id} not found",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            mapper.Map(request, existingStudent);
            var updatedStudent = await studentRepository.UpdateAsync(existingStudent, cancellationToken);

            var responseData = mapper.Map<object>(updatedStudent);

            return Response.SuccessResponse(
                responseData,
                "Student updated successfully",
                HttpStatusCode.OK
            );
        }
    }
}