
using UniversitySystem.Global;
using MediatR;

namespace UniversitySystem.Features.Student.Command.Models
{
    public class CreateStudentDto : IRequest<Response>
    {
        public string Sname { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}