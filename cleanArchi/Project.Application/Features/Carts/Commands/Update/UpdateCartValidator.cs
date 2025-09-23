using FluentValidation;

namespace Project.Application.Features.Carts.Commands.Update;

public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
{
    public UpdateCartValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Cart ID is required.");

        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}