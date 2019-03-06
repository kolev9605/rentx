using Microsoft.AspNetCore.Http;
using Rentx.Web.ImageWriter.Interfaces;
using System.Threading.Tasks;

namespace Rentx.Web.ImageWriter
{
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter imageWriter;

        public ImageHandler(IImageWriter imageWriter)
        {
            this.imageWriter = imageWriter;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var fileName = await this.imageWriter.UploadImage(file);
            return fileName;
        }
    }
}
