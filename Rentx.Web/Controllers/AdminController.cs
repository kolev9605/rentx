using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Common;
using Rentx.Web.Models.Admin;
using Rentx.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Administrator)]
    public class AdminController : Controller
    {
        private readonly IProductService productService;

        public AdminController(IProductService productService)
        {
            this.productService = productService;
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

            var success = await this.productService.UpdateAsync(model);
            return RedirectToAction("Index");
        }
    }
}