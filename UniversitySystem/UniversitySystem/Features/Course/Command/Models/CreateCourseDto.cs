
using UniversitySystem.Global;
using MediatR;

namespace UniversitySystem.Features.Course.Command.Models
{
    public class CreateCourseDto : IRequest<Response>
    {
        public string Code { get; set; } = string.Empty;
        public string Cname { get; set; } = string.Empty;
        public int Hours { get; set; }
    }
}
