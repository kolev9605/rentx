using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Rentx.Web.ImageWriter
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file);
    }
}
