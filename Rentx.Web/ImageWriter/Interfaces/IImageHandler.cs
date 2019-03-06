using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.ImageWriter.Interfaces
{
    public interface IImageHandler
    {
        Task<string> UploadImage(IFormFile file);
    }
}
