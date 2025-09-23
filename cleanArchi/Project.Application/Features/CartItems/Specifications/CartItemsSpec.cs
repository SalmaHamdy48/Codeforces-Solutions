using Ardalis.Specification;
using Project.Domain.Models;
using Project.Domain.Models.CartItems;
using Project.Domain.Models.Products;

namespace Project.Application.Features.CartItems.Specifications;

public class CartItemsSpec : Specification<CartItem>
{
    public CartItemsSpec(string? productName, int pageSize, int pageNumber)
    {
        if (productName != null)
            Query.Where(x => x.Product.Name.Contains(productName));
        Query.Skip(pageSize * (pageNumber - 1));
        Query.Take(pageSize);
    }
}