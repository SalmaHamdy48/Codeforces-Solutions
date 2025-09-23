using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.CartItems.Commands.Update;

public record UpdateCartItemCommand(Guid Id, Guid CartId, Guid ProductId, int Quantity) : ICommand<Guid>;