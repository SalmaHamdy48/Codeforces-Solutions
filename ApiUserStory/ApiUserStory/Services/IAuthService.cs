using ApiUserStory.Models;

namespace ApiUserStory.Services
{
    public interface IAuthService
    {
        Task SendEmailConfirmationOtp(ApplicationUser user);
        Task<ApiResult<string>> VerifyEmailAsync(string email, string otp);
        Task<ApiResult<string>> SendForgetPasswordOtpAsync(string email);
        Task<ApiResult<string>> VerifyForgetPasswordAsync(string email, string otp);
        Task<ApiResult<string>> ChangePasswordAsync(string sessionId, string newPassword);
    }
}
