using FluentValidation;

namespace Project.Application.Features.Carts.Commands.Delete;

public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Cart ID is required.");
    }
}