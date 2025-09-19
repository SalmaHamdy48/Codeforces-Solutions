
using MediatR;
using UniversitySystem.Global;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Course.Query.Models
{
    public class GetAllCoursesQuery : IRequest<Response>
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}