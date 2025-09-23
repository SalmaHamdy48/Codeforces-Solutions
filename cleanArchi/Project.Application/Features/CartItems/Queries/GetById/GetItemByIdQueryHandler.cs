using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.CartItems.Dtos;
using Project.Domain.Models;
using Project.Domain.Models.CartItems;
using Project.Domain.Responses;

namespace Project.Application.Features.CartItems.Queries.GetById;

public class GetItemByIdQueryHandler(IMapper mapper, IReadRepository<CartItem> cartItemRepository) : IQueryHandler<GetItemByIdQuery, CartItemDto>
{
    public async Task<Response<CartItemDto>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cartItem == null || cartItem.IsDeleted)
        {
            return Response<CartItemDto>.NotFound("Cart item not found");
        }

        var cartItemDto = mapper.Map<CartItemDto>(cartItem);
        return Response<CartItemDto>.Success(cartItemDto);
    }
}