
using MediatR;
using UniversitySystem.Global;
namespace UniversitySystem.Features.Student.Command.Models
{
    public class UpdateStudentDto : IRequest<Response>
    {
        public int Id { get; set; }
        public string Sname { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}