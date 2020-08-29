namespace TechAndTools.Services.Upload
{
    using Microsoft.AspNetCore.Http;

    using System.Threading.Tasks;

    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}
