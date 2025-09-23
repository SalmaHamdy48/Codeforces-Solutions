namespace Project.Application.Features.CartItems.Dtos;

public record CartItemDto(Guid Id, Guid CartId, Guid ProductId, int Quantity);