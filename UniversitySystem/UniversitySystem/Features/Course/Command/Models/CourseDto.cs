using MediatR;
using UniversitySystem.Global;

namespace UniversitySystem.Features.Course.Command.Models
{
    public record CourseDto( int Id, string Code, string Cname, int Hours ) : IRequest<Response>;
}


