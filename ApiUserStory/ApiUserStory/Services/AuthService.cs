using System.Text;
using System.Text.Json;
using ApiUserStory.Models;
using ApiUserStory.Dto;
using Microsoft.AspNetCore.Identity;

namespace ApiUserStory.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailService _emailService;

        public AuthService(UserManager<ApplicationUser> userManager, EmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task SendEmailConfirmationOtp(ApplicationUser user)
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();

            await _emailService.SendEmailAsync(
                user.Email,
                "Email Confirmation OTP",
                $"Your OTP is: {otp}"
            );
        }

        public async Task<ApiResult<string>> VerifyEmailAsync(string email, string otp)
        {
            return ApiResult<string>.SuccessResult("Email verified successfully");
        }

        public async Task<ApiResult<string>> SendForgetPasswordOtpAsync(string email)
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();

            await _emailService.SendEmailAsync(
                email,
                "Forget Password OTP",
                $"Your OTP for password reset is: {otp}"
            );

            return ApiResult<string>.SuccessResult("Forget password OTP sent");
        }

        public async Task<ApiResult<string>> VerifyForgetPasswordAsync(string email, string otp)
        {
            var sessionObject = new SessionData
            {
                Email = email,
                Expiration = DateTime.UtcNow.AddMinutes(5)
            };

            var json = JsonSerializer.Serialize(sessionObject);
            var sessionId = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            return ApiResult<string>.SuccessResult(sessionId);
        }

        public async Task<ApiResult<string>> ChangePasswordAsync(string sessionId, string newPassword)
        {
            try
            {
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(sessionId));
                var session = JsonSerializer.Deserialize<SessionData>(json);

                if (session == null || session.Expiration < DateTime.UtcNow)
                    return ApiResult<string>.Failure("Session expired");

                var user = await _userManager.FindByEmailAsync(session.Email);
                if (user == null)
                    return ApiResult<string>.Failure("User not found");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

                if (!result.Succeeded)
                    return ApiResult<string>.Failure("Failed to change password");

                return ApiResult<string>.SuccessResult("Password changed successfully");
            }
            catch
            {
                return ApiResult<string>.Failure("Invalid session");
            }
        }
    }
}
