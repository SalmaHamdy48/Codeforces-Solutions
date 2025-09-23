using FluentValidation;
using Project.Domain.Models.CartItems;

namespace Project.Application.Features.CartItems.Commands.Update;

public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemCommand>
{
    public UpdateCartItemValidator()
    {
        RuleFor(ci => ci.Id)
            .NotEmpty().WithMessage("Cart item ID is required.");

        RuleFor(ci => ci.CartId)
            .NotEmpty().WithMessage("Cart ID is required.");

        RuleFor(ci => ci.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(ci => ci.Quantity)
            .GreaterThanOrEqualTo(CartItemConstants.MinQuantityPerItem)
            .WithMessage($"Quantity must be at least {CartItemConstants.MinQuantityPerItem}.")
            .LessThanOrEqualTo(CartItemConstants.MaxQuantityPerItem)
            .WithMessage($"Quantity cannot exceed {CartItemConstants.MaxQuantityPerItem}.");
    }
}