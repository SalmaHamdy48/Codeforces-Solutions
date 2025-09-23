
using MediatR;
using UniversitySystem.Global;
using Response = UniversitySystem.Global.Response;

public class GetCourseByIdQuery : IRequest<Response>
{
    public int Id { get; }

    public GetCourseByIdQuery(int id)
    {
        Id = id;
    }
}
