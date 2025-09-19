
using FluentValidation;
using UniversitySystem.Features.Student.Command.Models;

namespace UniversitySystem.Features.Student.Command.Validators
{
    public class DeleteStudentValidator : AbstractValidator<DeleteStudentDto>
    {
        public DeleteStudentValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Student ID must be greater than 0");
        }
    }
}