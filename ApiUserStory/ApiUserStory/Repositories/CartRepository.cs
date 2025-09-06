using ApiUserStory.Data;
using ApiUserStory.Models;

namespace ApiUserStory.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public CartItem AddToCart(int productId, string userId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId && p.IsApproved);
            if (product == null) return null;

            var cartItem = new CartItem { UserId = userId, ProductId = productId };
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
            return cartItem;
        }
    }
}