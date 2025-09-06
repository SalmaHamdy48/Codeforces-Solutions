using ApiUserStory.Data;
using ApiUserStory.Models;

namespace ApiUserStory.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product CreateProduct(Product product)
        {
            product.IsApproved = false;
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product ApproveProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return null;

            product.IsApproved = true;
            _context.SaveChanges();
            return product;
        }

        public List<Product> GetApprovedProducts()
        {
            return _context.Products.Where(p => p.IsApproved).ToList();
        }
    }
}