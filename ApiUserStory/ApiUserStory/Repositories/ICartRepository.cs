using ApiUserStory.Models;

namespace ApiUserStory.Repositories
{
    public interface ICartRepository
    {
        CartItem AddToCart(int productId, string userId);
    }
}