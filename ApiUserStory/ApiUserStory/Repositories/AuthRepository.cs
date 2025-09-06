using ApiUserStory.Models;
using ApiUserStory.Services;
using Microsoft.AspNetCore.Identity;

namespace ApiUserStory.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public AuthRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<ApiResult<string>> RegisterAsync(string username, string password, string role, string email)
        {
            var user = new ApplicationUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return ApiResult<string>.Failure("Registration failed: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);

            await _authService.SendEmailConfirmationOtp(user);

            return ApiResult<string>.SuccessResult("User registered, OTP sent");
        }

        public async Task<ApiResult<string>> VerifyEmailAsync(string email, string otp)
        {
            return await _authService.VerifyEmailAsync(email, otp);
        }

        public async Task<ApiResult<string>> SendForgetPasswordOtpAsync(string email)
        {
            return await _authService.SendForgetPasswordOtpAsync(email);
        }

        public async Task<ApiResult<string>> VerifyForgetPasswordAsync(string email, string otp)
        {
            return await _authService.VerifyForgetPasswordAsync(email, otp);
        }

        public async Task<ApiResult<string>> ChangePasswordAsync(string sessionId, string newPassword)
        {
            return await _authService.ChangePasswordAsync(sessionId, newPassword);
        }
    }
}
