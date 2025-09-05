using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiUserStory.Data;
using ApiUserStory.Models;

namespace ApiUserStory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "ProductCreator")]
        public IActionResult CreateProduct(Product product)
        {
            product.IsApproved = false;
            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(ApiResult<Product>.SuccessResult(product, "Product created successfully"));
        }

        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public IActionResult ApproveProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) 
                return NotFound(ApiResult<string>.Failure("Product not found"));

            product.IsApproved = true;
            _context.SaveChanges();

            return Ok(ApiResult<Product>.SuccessResult(product, "Product approved successfully"));
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult GetApprovedProducts()
        {
            var products = _context.Products.Where(p => p.IsApproved).ToList();
            return Ok(ApiResult<List<Product>>.SuccessResult(products, "Approved products fetched"));
        }
    }
}