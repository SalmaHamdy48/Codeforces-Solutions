using MediatR;

namespace UniversitySystem.Features.Student.Command.Models;

public class CreateStudentDto : IRequest<UniversitySystem.Models.Student>
{
    public string Sname { get; set; }
    public int Age { get; set; }
}