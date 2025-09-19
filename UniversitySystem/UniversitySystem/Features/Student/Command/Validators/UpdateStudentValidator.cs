
using FluentValidation;
using UniversitySystem.Features.Student.Command.Models;

namespace UniversitySystem.Features.Student.Command.Validators
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Student ID must be greater than 0");

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