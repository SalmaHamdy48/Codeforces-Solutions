using MediatR;
using UniversitySystem.Features.Student.Command.Models;
using UniversitySystem.Repositories.Interfaces;

namespace UniversitySystem.Features.Student.Command.Handlers;

public class CreateStudentHandler(IStudentRepository repo)
    : IRequestHandler<CreateStudentDto, UniversitySystem.Models.Student>
{
    public async Task<UniversitySystem.Models.Student> Handle(CreateStudentDto request, CancellationToken cancellationToken)
    {
        var student = new UniversitySystem.Models.Student
        {
            Sname = request.Sname,
            Age = request.Age
        };

        await repo.AddAsync(student);
        await repo.SaveChangesAsync();

        return student;
    }
}