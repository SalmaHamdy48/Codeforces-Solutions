using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Features.Student.Query.Models;
using UniversitySystem.Global;
using System.Net;
using AutoMapper;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Repositories.Interfaces;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Query.Handlers
{
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Response>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentByIdHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (student == null)
            {
                return Response.ErrorResponse(
                    $"Student with ID {request.Id} not found",
                    statusCode: HttpStatusCode.NotFound
                );
            }

            var studentData = _mapper.Map<StudentDto>(student);

            var responseData = new
            {
                Student = studentData,
                Message = "Student retrieved successfully"
            };

            return Response.SuccessResponse(
                responseData,
                "Student retrieved successfully",
                HttpStatusCode.OK
            );
        }
    }
}