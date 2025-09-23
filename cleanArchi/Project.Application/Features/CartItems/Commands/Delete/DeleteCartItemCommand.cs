using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.CartItems.Commands.Delete;

public record DeleteCartItemCommand(Guid Id) : ICommand<Guid>;