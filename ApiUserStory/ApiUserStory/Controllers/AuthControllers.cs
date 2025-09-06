using Microsoft.AspNetCore.Mvc;
using ApiUserStory.Models;
using ApiUserStory.Repositories;

namespace ApiUserStory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password, string role, string email)
        {
            var result = await _authRepository.RegisterAsync(username, password, role, email);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(string email, string otp)
        {
            var result = await _authRepository.VerifyEmailAsync(email, otp);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var result = await _authRepository.SendForgetPasswordOtpAsync(email);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("verify-forget-password")]
        public async Task<IActionResult> VerifyForgetPassword(string email, string otp)
        {
            var result = await _authRepository.VerifyForgetPasswordAsync(email, otp);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(string sessionId, string newPassword)
        {
            var result = await _authRepository.ChangePasswordAsync(sessionId, newPassword);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}