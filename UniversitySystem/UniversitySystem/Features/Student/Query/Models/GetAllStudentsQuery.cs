using Azure;
using MediatR;
using UniversitySystem.Global;
using Response = UniversitySystem.Global.Response;

namespace UniversitySystem.Features.Student.Query.Models
{
    public class GetAllStudentsQuery : IRequest<Response>
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}