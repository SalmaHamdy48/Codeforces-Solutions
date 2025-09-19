
using FluentValidation;
using UniversitySystem.Features.Student.Command.Models;

namespace UniversitySystem.Features.Student.Command.Validators
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.Sname)
                .NotEmpty()
                .WithMessage("Student name is required")
                .MaximumLength(50)
                .WithMessage("Student name cannot exceed 50 characters");

            RuleFor(x => x.Age)
                .InclusiveBetween(16, 100)
                .WithMessage("Student age must be between 16 and 100");
        }
    }
}