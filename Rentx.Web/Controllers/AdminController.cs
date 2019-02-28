using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Common;
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
            var products = await this.productService.GetAllProductsAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var success = await this.productService.DeleteProductByIdAsync(productId);

            return RedirectToAction("Index");
        }
    }
}
