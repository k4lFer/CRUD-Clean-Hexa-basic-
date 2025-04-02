using Application.Interfaces.ExternalServices;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.ExternalServices
{
    public class CloudinaryService : ICloudinaryService
    {
        public Task<bool> DeleteImageAsync(string publicId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadImageAsync(IFormFile file, string folderName, string publicId)
        {
            throw new NotImplementedException();
        }
    }
}