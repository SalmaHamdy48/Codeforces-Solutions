using MediatR;
using UniversitySystem.Global;
using UniversitySystem.Features.Student.Query.Models;

namespace UniversitySystem.Features.Student.Query.Models
{
    public class GetStudentByIdQuery : IRequest<Response>
    {
        public int Id { get; set; }
    }
}