using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Common;
using Rentx.Web.Common.Interfaces;
using Rentx.Web.ImageWriter;
using Rentx.Web.ImageWriter.Interfaces;
using Rentx.Web.Models.Admin;
using Rentx.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Administrator)]
    public class AdminController : Controller
    {
        private readonly IProductService productService;
        private readonly IImageHandler imageHandler;
        private readonly IPathHelper pathHelper;

        public AdminController(IProductService productService, IImageHandler imageHandler, IPathHelper pathHelper)
        {
            this.productService = productService;
            this.imageHandler = imageHandler;
            this.pathHelper = pathHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await this.productService.GetAllAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var success = await this.productService.DeleteByIdAsync(productId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var image = await this.imageHandler.UploadImage(model.Image);
            model.ImageFileName = image;

            await this.productService.AddAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await this.productService.GetByIdAsync(productId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var image = await this.imageHandler.UploadImage(model.Image);

            model.ImageFileName = image;

            var success = await this.productService.UpdateAsync(model);
            return RedirectToAction("Index");
        }
    }
}