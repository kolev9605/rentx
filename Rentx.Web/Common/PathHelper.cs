using Microsoft.AspNetCore.Hosting;
using Rentx.Web.Common.Interfaces;
using System;
using System.IO;

namespace Rentx.Web.Common
{
    public class PathHelper : IPathHelper
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public PathHelper(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public string GetImagePath(string fileName)
        {
            string webRootPath = this.hostingEnvironment.WebRootPath;
            var path = Path.Combine(webRootPath, "images", fileName);

            return path;
        }

        public string GetImagesRootPath()
        {
            string webRootPath = this.hostingEnvironment.WebRootPath;

            return webRootPath;
        }
    }
}
