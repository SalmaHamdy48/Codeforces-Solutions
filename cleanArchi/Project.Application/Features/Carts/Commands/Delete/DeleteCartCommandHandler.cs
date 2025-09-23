using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Commands.Delete;

public class DeleteCartCommandHandler(IRepository<Cart> cartRepository) : ICommandHandler<DeleteCartCommand, Guid>
{
    public async Task<Response<Guid>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart == null)
        {
            return Response<Guid>.NotFound("Cart not found.");
        }

        await cartRepository.DeleteAsync(cart, cancellationToken);
        return Response<Guid>.Success(request.Id);
    }
}