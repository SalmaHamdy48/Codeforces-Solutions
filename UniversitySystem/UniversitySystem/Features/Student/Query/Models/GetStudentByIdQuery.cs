using MediatR;
using UniversitySystem.Features.Student.Query.Models;
using UniversitySystem.Features.Student.Query.Handlers;

namespace UniversitySystem.Features.Student.Query.Models
{
    public class GetStudentByIdQuery : IRequest<StudentResponseDto>
    {
        public int Id { get; set; }
    }
}