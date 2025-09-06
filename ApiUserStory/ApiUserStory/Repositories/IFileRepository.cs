namespace ApiUserStory.Repositories;

public interface IFileRepository
{
    Task<string> UploadFileAsync(IFormFile file, HttpRequest request);
}