using Microsoft.AspNetCore.Http;
using Rentx.Web.Common.Interfaces;
using Rentx.Web.ImageWriter.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Rentx.Web.ImageWriter
{
    public class ImageWriter : IImageWriter
    {
        private readonly IPathHelper pathHelper;

        public ImageWriter(IPathHelper pathHelper)
        {
            this.pathHelper = pathHelper;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if (CheckIfImageFile(file))
            {
                var fileName = await WriteFile(file);
                return fileName;
            }

            return "Invalid image file";
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> WriteFile(IFormFile file)
        {
            string fileName = string.Empty;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString() + extension; //Create a new Name for the file due to security reasons.
                
                var path = this.pathHelper.GetImagePath(fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return fileName;
        }
    }
}