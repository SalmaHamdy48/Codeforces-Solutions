using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Carts.Commands.Add;

public record AddCartCommand(Guid UserId) : ICommand<Guid>;
