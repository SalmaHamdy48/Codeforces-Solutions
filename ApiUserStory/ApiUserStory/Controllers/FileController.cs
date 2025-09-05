using Microsoft.AspNetCore.Mvc;

namespace ApiUserStory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly LinkGenerator _linkGenerator;

    public FileController(IWebHostEnvironment env, LinkGenerator linkGenerator)
    {
        _env = env;
        _linkGenerator = linkGenerator;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        // Path where files will be stored
        var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        // Save file
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Resolve absolute URL for the file
        var url = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

        return Ok(new { FileUrl = url });
    }
}