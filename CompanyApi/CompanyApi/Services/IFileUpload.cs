using Microsoft.AspNetCore.Http;


namespace CompanyApi.Services;


public interface IFileUpload
{
    Task<string?> SaveAsync(IFormFile? file, string subFolder);
    Task DeleteIfExistsAsync(string? fileName, string subFolder);
}