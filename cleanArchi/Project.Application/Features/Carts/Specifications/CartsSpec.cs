using Ardalis.Specification;
using Project.Domain.Models.Carts;

namespace Project.Application.Features.Carts.Specifications;

public class CartsSpec : Specification<Cart>
{
    public CartsSpec(string? userId, int pageSize, int pageNumber)
    {
        if (userId != null)
            Query.Where(x => x.UserId.ToString() == userId);
        Query.Skip(pageSize * (pageNumber - 1));
        Query.Take(pageSize);
    }
}