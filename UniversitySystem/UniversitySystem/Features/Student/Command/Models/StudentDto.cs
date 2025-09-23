using MediatR;
using UniversitySystem.Global;

namespace UniversitySystem.Features.Student.Command.Models
{
    public record StudentDto(int Id, string Sname, int Age)  : IRequest<Response>;
}

