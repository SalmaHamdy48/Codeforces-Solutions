
using MediatR;
using UniversitySystem.Global;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Query.Models
{
    public class GetCourseByIdQuery : IRequest<Response>
    {
        public int Id { get; set; }
    }
}