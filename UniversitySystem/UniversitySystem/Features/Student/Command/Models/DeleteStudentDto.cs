using UniversitySystem.Global;
using MediatR;

namespace UniversitySystem.Features.Student.Command.Models
{
    public class DeleteStudentDto(int id) : IRequest<Response>
    {
        public int Id { get; set; }
    }
}
