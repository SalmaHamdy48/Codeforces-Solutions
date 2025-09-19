
using FluentValidation;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Command.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversitySystem.Features.Course.Command.Validators
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseDto>
    {
        private readonly ApplicationDbContext _context;

        public CreateCourseValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Course code is required")
                .MaximumLength(10)
                .WithMessage("Course code cannot exceed 10 characters")
                .MustAsync(BeUniqueCode)
                .WithMessage("Course code already exists. Please use a unique code.");

            RuleFor(x => x.Cname)
                .NotEmpty()
                .WithMessage("Course name is required")
                .MaximumLength(100)
                .WithMessage("Course name cannot exceed 100 characters");

            RuleFor(x => x.Hours)
                .InclusiveBetween(1, 10)
                .WithMessage("Course hours must be between 1 and 10");
        }

        private async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
        {
            return await _context.Courses.AllAsync(c => c.Code != code, cancellationToken);
        }
    }
}