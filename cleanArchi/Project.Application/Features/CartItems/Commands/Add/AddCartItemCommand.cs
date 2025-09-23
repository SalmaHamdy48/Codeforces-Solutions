using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.CartItems.Commands.Add;

public record AddCartItemCommand(Guid CartId, Guid ProductId, int Quantity, Guid CreatedBy) : ICommand<Guid>;
