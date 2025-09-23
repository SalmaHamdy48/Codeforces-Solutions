using Project.Application.Abstractions.Messaging;
using Project.Application.Features.CartItems.Dtos;
using Project.Domain.Filters;
using Project.Domain.Responses;

namespace Project.Application.Features.CartItems.Queries.GetAll;

public record GetAllItemsQuery(string? ProductName) : BaseFilter, IQuery<PaginatedResult<CartItemDto>>;