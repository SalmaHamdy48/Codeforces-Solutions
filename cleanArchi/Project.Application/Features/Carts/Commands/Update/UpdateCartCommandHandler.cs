using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Commands.Update;

public class UpdateCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository) : ICommandHandler<UpdateCartCommand, Guid>
{
    public async Task<Response<Guid>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(request.Id, cancellationToken);
        if (cart == null)
        {
            return Response<Guid>.NotFound("Cart not found.");
        }

        mapper.Map(request, cart);
        await cartRepository.UpdateAsync(cart, cancellationToken);
        return Response<Guid>.Success(request.Id);
    }
}