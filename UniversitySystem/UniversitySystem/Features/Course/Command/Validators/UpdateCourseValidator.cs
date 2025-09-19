
using FluentValidation;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Command.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversitySystem.Features.Course.Command.Validators
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseDto>
    {
        private readonly ApplicationDbContext _context;

        public UpdateCourseValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Course ID must be greater than 0")
                .MustAsync(CourseExists)
                .WithMessage("Course with the specified ID does not exist.");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Course code is required")
                .MaximumLength(10)
                .WithMessage("Course code cannot exceed 10 characters");

            RuleFor(x => x.Cname)
                .NotEmpty()
                .WithMessage("Course name is required")
                .MaximumLength(100)
                .WithMessage("Course name cannot exceed 100 characters");

            RuleFor(x => x.Hours)
                .InclusiveBetween(1, 10)
                .WithMessage("Course hours must be between 1 and 10");
        }

        private async Task<bool> CourseExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Courses.AnyAsync(c => c.Id == id, cancellationToken);
        }
        
    }
}