using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ApiUserStory.Models;

namespace ApiUserStory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password, string role)
        {
            var user = new ApplicationUser { UserName = username, Email = $"{username}@test.com" };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return BadRequest(ApiResult<string>.Failure("Registration failed: " +
                    string.Join(", ", result.Errors.Select(e => e.Description))));

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);

            return Ok(ApiResult<string>.SuccessResult("User registered successfully", "Registration done"));
        }
    }
}
