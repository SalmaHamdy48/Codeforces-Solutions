using FluentValidation;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models;
using Project.Domain.Models.Categories;
using Project.Domain.Models.Products;

namespace Project.Application.Features.Products.Commands.Update;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IReadRepository<Category> _categoryRepository;

    public UpdateProductValidator(IReadRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(p => p.Id)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(ProductConstants.ProductNameMaxLengthValue)
            .WithMessage(ProductConstants.ProductNameMaxLengthMessage);

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithMessage("Category ID is required.");
    }
}