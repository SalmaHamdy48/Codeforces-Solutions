using ApiUserStory.Models;

namespace ApiUserStory.Repositories
{
    public interface IProductRepository
    {
        Product CreateProduct(Product product);
        Product ApproveProduct(int id);
        List<Product> GetApprovedProducts();
    }
}