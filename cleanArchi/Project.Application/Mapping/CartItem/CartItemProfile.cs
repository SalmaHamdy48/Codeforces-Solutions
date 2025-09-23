using AutoMapper;
using Project.Application.Features.CartItems.Commands.Add;
using Project.Application.Features.CartItems.Commands.Update;
using Project.Application.Features.CartItems.Dtos;
using Project.Domain.Models.CartItems;

namespace Project.Application.Mapping.CartItem;

public class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<AddCartItemCommand, Domain.Models.CartItems.CartItem>();
        CreateMap<UpdateCartItemCommand, Domain.Models.CartItems.CartItem>();
        CreateMap<Domain.Models.CartItems.CartItem, CartItemDto>();
    }
}