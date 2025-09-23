using Project.Application.Features.CartItems.Dtos;

namespace Project.Application.Features.Carts.Dtos;

public record CartDto(Guid Id, List<CartItemDto> Items);