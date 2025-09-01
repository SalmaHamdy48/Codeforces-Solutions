using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace CompanyApi.Services;


public class FileUpload(IWebHostEnvironment env) : IFileUpload
{
    private readonly IWebHostEnvironment _env = env;


    public async Task<string?> SaveAsync(IFormFile? file, string subFolder)
    {
        if (file == null || file.Length == 0) return null;
        var root = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), subFolder);
        Directory.CreateDirectory(root);
        var ext = Path.GetExtension(file.FileName);
        var name = $"{Guid.NewGuid()}{ext}";
        var fullPath = Path.Combine(root, name);
        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);
        return name;
    }


    public Task DeleteIfExistsAsync(string? fileName, string subFolder)
    {
        if (string.IsNullOrWhiteSpace(fileName)) return Task.CompletedTask;
        var root = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), subFolder);
        var full = Path.Combine(root, fileName);
        if (File.Exists(full)) File.Delete(full);
        return Task.CompletedTask;
    }
}