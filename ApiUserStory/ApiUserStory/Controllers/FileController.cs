using ApiUserStory.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiUserStory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileRepository _fileRepository;

    public FileController(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var fileUrl = await _fileRepository.UploadFileAsync(file, Request);
        return Ok(new { FileUrl = fileUrl });
    }
}