using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.CartItems;
using Project.Domain.Responses;

namespace Project.Application.Features.CartItems.Commands.Delete;

public class DeleteCartItemCommandHandler(IRepository<CartItem> cartItemRepository) : ICommandHandler<DeleteCartItemCommand, Guid>
{
    public async Task<Response<Guid>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cartItem == null)
        {
            return Response<Guid>.NotFound("Cart item not found.");
        }

        await cartItemRepository.DeleteAsync(cartItem, cancellationToken);
        return Response<Guid>.Success(request.Id);
    }
}