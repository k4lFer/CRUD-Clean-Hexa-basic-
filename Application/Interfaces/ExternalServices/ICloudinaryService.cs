using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.ExternalServices
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file, string folderName, string publicId);
        Task<bool> DeleteImageAsync(string publicId);
    }
}