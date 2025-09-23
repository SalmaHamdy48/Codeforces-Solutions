using Project.Application.Abstractions.Messaging;
using Project.Application.Features.CartItems.Dtos;

namespace Project.Application.Features.CartItems.Queries.GetById;

public record GetItemByIdQuery(Guid Id) : IQuery<CartItemDto>;