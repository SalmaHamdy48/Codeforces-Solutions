using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Carts.Commands.Update;

public record UpdateCartCommand(Guid Id, Guid UserId) : ICommand<Guid>;