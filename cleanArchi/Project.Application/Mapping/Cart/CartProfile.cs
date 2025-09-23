using AutoMapper;
using Project.Application.Features.Carts.Commands.Add;
using Project.Application.Features.Carts.Commands.Update;
using Project.Application.Features.Carts.Dtos;
using Project.Domain.Models.Carts;

namespace Project.Application.Mapping.Cart;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<AddCartCommand, Domain.Models.Carts.Cart>();
        CreateMap<UpdateCartCommand, Domain.Models.Carts.Cart>();
        CreateMap<Domain.Models.Carts.Cart, CartDto>();
    }
}