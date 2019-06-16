using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models.Admin;
using Rentx.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    /// <summary>
    /// Controller responsible for product operations
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Returns product details page
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int productId)
        {
            ProductViewModel product = await this.productService.GetByIdAsync(productId);

            return View(product);
        }
    }
}
