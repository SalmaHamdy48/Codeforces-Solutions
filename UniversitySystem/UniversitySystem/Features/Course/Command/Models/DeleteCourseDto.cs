using UniversitySystem.Global;
using MediatR;

namespace UniversitySystem.Features.Course.Command.Models
{
    public class DeleteCourseDto(int id) : IRequest<Response>
    {
        public int Id { get; set; } = id;
    }
}