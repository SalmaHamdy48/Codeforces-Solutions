using Microsoft.AspNetCore.Hosting;

namespace ApiUserStory.Repositories;

public class FileRepository : IFileRepository
{
    private readonly IWebHostEnvironment _env;

    public FileRepository(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> UploadFileAsync(IFormFile file, HttpRequest request)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file uploaded.");

        
        var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        
        var url = $"{request.Scheme}://{request.Host}/uploads/{fileName}";

        return url;
    }
}