using FluentValidation;

namespace Project.Application.Features.CartItems.Commands.Delete;

public class DeleteCartItemValidator : AbstractValidator<DeleteCartItemCommand>
{
    public DeleteCartItemValidator()
    {
        RuleFor(ci => ci.Id)
            .NotEmpty().WithMessage("Cart item ID is required.");
    }
}