using UniversitySystem.Global;
using MediatR;

namespace UniversitySystem.Features.Course.Command.Models
{
    public record DeleteCourseDto(int Id) : IRequest<Response>;
}