using FluentValidation;

namespace Project.Application.Features.CartItems.Queries.GetById;

public class GetItemByIdValidator : AbstractValidator<GetItemByIdQuery>
{
    public GetItemByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}