using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Carts;
using Project.Domain.Responses;

namespace Project.Application.Features.Carts.Commands.Add;

public class AddCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository) : ICommandHandler<AddCartCommand, Guid>
{
    public async Task<Response<Guid>> Handle(AddCartCommand request, CancellationToken cancellationToken)
    {
        var cart = mapper.Map<Cart>(request);
        await cartRepository.AddAsync(cart, cancellationToken);
        return Response<Guid>.Success(cart.Id);
    }
}