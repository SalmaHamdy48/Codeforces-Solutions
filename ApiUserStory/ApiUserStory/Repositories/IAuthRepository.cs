using ApiUserStory.Models;

namespace ApiUserStory.Repositories
{
    public interface IAuthRepository
    {
        Task<ApiResult<string>> RegisterAsync(string username, string password, string role, string email);
        Task<ApiResult<string>> VerifyEmailAsync(string email, string otp);
        Task<ApiResult<string>> SendForgetPasswordOtpAsync(string email);
        Task<ApiResult<string>> VerifyForgetPasswordAsync(string email, string otp);
        Task<ApiResult<string>> ChangePasswordAsync(string sessionId, string newPassword);
    }
}
