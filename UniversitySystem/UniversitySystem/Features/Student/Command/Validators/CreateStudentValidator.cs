using FluentValidation;
using UniversitySystem.Features.Student.Command.Models;

namespace UniversitySystem.Features.Student.Command.Validators;

public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentValidator()
    {
        RuleFor(x => x.Sname).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Age).InclusiveBetween(18, 100);
    }
}