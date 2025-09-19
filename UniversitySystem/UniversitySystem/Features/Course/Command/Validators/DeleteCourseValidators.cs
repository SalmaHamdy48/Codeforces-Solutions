
using FluentValidation;
using UniversitySystem.Data;
using UniversitySystem.Features.Course.Command.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversitySystem.Features.Course.Command.Validators
{
    public class DeleteCourseValidator : AbstractValidator<DeleteCourseDto>
    {
        private readonly ApplicationDbContext _context;

        public DeleteCourseValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Course ID must be greater than 0")
                .MustAsync(CourseExists)
                .WithMessage("Course with the specified ID does not exist.");
        }

        private async Task<bool> CourseExists(int id, CancellationToken cancellationToken)
        {
            return await _context.Courses.AnyAsync(c => c.Id == id, cancellationToken);
        }
    }
}