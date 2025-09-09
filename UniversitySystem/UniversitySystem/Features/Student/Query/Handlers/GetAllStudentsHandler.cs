using MediatR;
using AutoMapper;
using UniversitySystem.Repositories.Interfaces;
using UniversitySystem.Features.Student.Query.Models;

namespace UniversitySystem.Features.Student.Query.Handlers
{
    public class GetAllStudentsHandler(IStudentRepository studentRepository, IMapper mapper)
        : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentListResponseDto>>
    {
        public async Task<IEnumerable<StudentListResponseDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await studentRepository.GetAllAsync();
            return mapper.Map<IEnumerable<StudentListResponseDto>>(students);
        }
    }
}