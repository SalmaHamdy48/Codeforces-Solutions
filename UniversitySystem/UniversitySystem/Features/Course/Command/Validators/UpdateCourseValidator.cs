using FluentValidation;
using UniversitySystem.Features.Course.Command.Models;

namespace UniversitySystem.Features.Course.Command.Validators;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseDto>
{
    public UpdateCourseValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Course Id must be greater than 0");

        RuleFor(x => x.Cname)
            .NotEmpty().WithMessage("Course name is required")
            .MaximumLength(100).WithMessage("Course name must not exceed 100 characters");

        RuleFor(x => x.Hours)
            .InclusiveBetween(1, 10).WithMessage("Course hours must be between 1 and 10");
    }
}