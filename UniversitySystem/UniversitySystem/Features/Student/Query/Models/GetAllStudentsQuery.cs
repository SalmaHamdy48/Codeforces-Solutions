using MediatR;
using System.Collections.Generic;
using UniversitySystem.Features.Student.Query.Models;

namespace UniversitySystem.Features.Student.Query.Handlers
{
    public class GetAllStudentsQuery : IRequest<IEnumerable<StudentListResponseDto>>
    {
    }
}