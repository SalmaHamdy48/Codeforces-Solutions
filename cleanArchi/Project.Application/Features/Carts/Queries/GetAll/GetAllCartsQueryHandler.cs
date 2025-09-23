using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Application.Features.Carts.Dtos;
using Project.Application.Features.Carts.Specifications;
using Project.Domain.Models;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Queries.GetAll;

public class GetAllCartsQueryHandler(IMapper mapper, IReadRepository<Cart> cartRepository) : IQueryHandler<GetAllCartsQuery, PaginatedResult<CartDto>>
{
    public async Task<Response<PaginatedResult<CartDto>>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
    {
        var carts = await cartRepository
            .ListAsync(new CartsSpec(request.UserId, 
                request.PageSize, 
                request.PageNumber), cancellationToken);

        var cartsCount = await cartRepository
            .CountAsync(new CartsSpec(request.UserId,
                request.PageSize,
                request.PageNumber), cancellationToken);
        
        var mappedCarts = mapper.Map<IEnumerable<CartDto>>(carts);
        
        return Response<CartDto>.GetData(mappedCarts, request.PageNumber, request.PageSize, cartsCount);
    }
}