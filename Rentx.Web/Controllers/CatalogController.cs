using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models.Catalog;
using Rentx.Web.Services.Interfaces;

namespace Rentx.Web.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public IActionResult Index()
        {
            var products = this.catalogService.GetCatalogProducts();
            CatalogProductsViewModel model = new CatalogProductsViewModel();
            model.ProductItems = products;
            return View(model);
        }
    }
}
