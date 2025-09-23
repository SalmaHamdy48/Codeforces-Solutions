using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.CartItems.Dtos;
using Project.Application.Features.CartItems.Specifications;
using Project.Domain.Models;
using Project.Domain.Models.CartItems;
using Project.Domain.Responses;

namespace Project.Application.Features.CartItems.Queries.GetAll;

public class GetAllItemsQueryHandler(IMapper mapper, IReadRepository<CartItem> cartItemRepository) : IQueryHandler<GetAllItemsQuery, PaginatedResult<CartItemDto>>
{
    public async Task<Response<PaginatedResult<CartItemDto>>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var cartItems = await cartItemRepository
            .ListAsync(new CartItemsSpec(request.ProductName, 
                request.PageSize, 
                request.PageNumber), cancellationToken);

        var cartItemsCount = await cartItemRepository
            .CountAsync(new CartItemsSpec(request.ProductName,
                request.PageSize,
                request.PageNumber), cancellationToken);
        
        var mappedCartItems = mapper.Map<IEnumerable<CartItemDto>>(cartItems);
        
        return Response<CartItemDto>.GetData(mappedCartItems, request.PageNumber, request.PageSize, cartItemsCount);
    }
}