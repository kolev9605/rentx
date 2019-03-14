using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models.Catalog;
using Rentx.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await this.catalogService.GetCatalogProductsAsync();

            return View(products);
        }
    }
}
