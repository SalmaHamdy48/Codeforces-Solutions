using MediatR;
using AutoMapper;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Features.Student.Query.Models;

namespace UniversitySystem.Features.Student.Query.Handlers
{
    public class GetStudentByIdHandler(IStudentRepository studentRepository, IMapper mapper)
        : IRequestHandler<GetStudentByIdQuery, StudentResponseDto>
    {
        public async Task<StudentResponseDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.GetByIdAsync(request.Id);
            return student != null ? mapper.Map<StudentResponseDto>(student) : null;
        }
    }
}