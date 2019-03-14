using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Details(int productId)
        {
            var product = await this.productService.GetByIdAsync(productId);

            return View(product);
        }
    }
}
