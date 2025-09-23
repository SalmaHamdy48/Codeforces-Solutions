using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.CartItems;
using Project.Domain.Responses;

namespace Project.Application.Features.CartItems.Commands.Add;

public class AddCartItemCommandHandler(IMapper mapper, IRepository<CartItem> cartItemRepository) : ICommandHandler<AddCartItemCommand, Guid>
{
    public async Task<Response<Guid>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartItem = mapper.Map<CartItem>(request);
        await cartItemRepository.AddAsync(cartItem, cancellationToken);
        return Response<Guid>.Success(cartItem.Id);
    }
}