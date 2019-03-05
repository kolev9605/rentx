using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.ImageWriter;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageHandler imageHandler;

        public ImagesController(IImageHandler imageHandler)
        {
            this.imageHandler = imageHandler;
        }

        /// <summary>
        /// Uplaods an image to the server.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return await this.imageHandler.UploadImage(file);
        }
    }
}
