using FluentValidation;
using UniversitySystem.Features.Student.Command.Models;

namespace UniversitySystem.Features.Student.Command.Validators;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Sname).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Age).InclusiveBetween(18, 100);
    }
}