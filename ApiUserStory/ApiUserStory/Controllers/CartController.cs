using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiUserStory.Models;
using ApiUserStory.Repositories;

namespace ApiUserStory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repo;

        public CartController(ICartRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("{productId}")]
        [Authorize(Roles = "User")]
        public IActionResult AddToCart(int productId, string userId)
        {
            var item = _repo.AddToCart(productId, userId);
            if (item == null)
                return BadRequest(ApiResult<string>.Failure("Product not found or not approved"));

            return Ok(ApiResult<CartItem>.SuccessResult("Added to cart", item));
        }
    }
}