using Project.Application.Abstractions.Messaging;

namespace Project.Application.Features.Categories.Commands.Update;

public record UpdateCategoryCommand(Guid Id, string Name) : ICommand<Guid>;