namespace Eventease.Services
{
    public interface IAzureService
    {
        Task<string> UploadFiles(IFormFile file);
    }
}
