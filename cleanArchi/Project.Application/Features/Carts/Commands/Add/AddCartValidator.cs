using FluentValidation;

namespace Project.Application.Features.Carts.Commands.Add;

public class AddCartValidator : AbstractValidator<AddCartCommand>
{
    public AddCartValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}