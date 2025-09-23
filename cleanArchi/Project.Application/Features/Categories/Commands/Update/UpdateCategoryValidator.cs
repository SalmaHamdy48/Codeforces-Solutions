using FluentValidation;
using Project.Domain.Models.Categories;

namespace Project.Application.Features.Categories.Commands.Update;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(CategoryConstants.CategoryNameMaxLengthValue)
            .WithMessage(CategoryConstants.CategoryNameMaxLengthMessage);
    }
}