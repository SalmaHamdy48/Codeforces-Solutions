using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.CartItems;
using Project.Domain.Responses;

namespace Project.Application.Features.CartItems.Commands.Update;

public class UpdateCartItemCommandHandler(IMapper mapper, IRepository<CartItem> cartItemRepository) : ICommandHandler<UpdateCartItemCommand, Guid>
{
    public async Task<Response<Guid>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cartItem == null)
        {
            return Response<Guid>.NotFound("Cart item not found");
        }

        mapper.Map(request, cartItem);
        await cartItemRepository.UpdateAsync(cartItem, cancellationToken);
        return Response<Guid>.Success(request.Id);
    }
}