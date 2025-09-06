using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiUserStory.Models;
using ApiUserStory.Repositories;

namespace ApiUserStory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Authorize(Roles = "ProductCreator")]
        public IActionResult CreateProduct(Product product)
        {
            var created = _repo.CreateProduct(product);
            return Ok(ApiResult<Product>.SuccessResult("Product created successfully",created));
        }

        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public IActionResult ApproveProduct(int id)
        {
            var product = _repo.ApproveProduct(id);
            if (product == null) 
                return NotFound(ApiResult<string>.Failure("Product not found"));

            return Ok(ApiResult<Product>.SuccessResult("Product approved successfully",product));
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult GetApprovedProducts()
        {
            var products = _repo.GetApprovedProducts();
            return Ok(ApiResult<List<Product>>.SuccessResult("Approved products fetched",products));
        }
    }
}