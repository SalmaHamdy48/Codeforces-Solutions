using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiUserStory.Data;
using ApiUserStory.Models;


namespace ApiUserStory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{productId}")]
        [Authorize(Roles = "User")]
        public IActionResult AddToCart(int productId, string userId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId && p.IsApproved);
            if (product == null) 
                return BadRequest(ApiResult<string>.Failure("Product not found or not approved"));

            var cartItem = new CartItem { UserId = userId, ProductId = productId };
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            return Ok(ApiResult<CartItem>.SuccessResult(cartItem, "Added to cart"));
        }
    }
}
