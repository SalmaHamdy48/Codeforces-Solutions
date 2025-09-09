using MediatR;

namespace UniversitySystem.Features.Student.Command.Models
{
    public class DeleteStudentDto : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
