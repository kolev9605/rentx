using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models.Catalog;
using Rentx.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    /// <summary>
    /// Controller responsible for Catalog operations
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ICatalogService catalogService;
        private readonly ICategoryService categoryService;

        public HomeController(ICatalogService catalogService, ICategoryService categoryService)
        {
            this.catalogService = catalogService;
            this.categoryService = categoryService;
        }

        /// <summary>
        /// The main catalog action. Returns products to the homepage by given category or keyword to search.
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <param name="searchTerm">Keyword to search for products in their titles</param>
        /// <returns>Products based on the search or the category passed</returns>
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]CatalogSearchRequest catalogSearchRequest)
        {
            var model = new CatalogViewModel();

            if (catalogSearchRequest.SearchTerm != null)
            {
                model.CatalogProducts = await this.catalogService.GetAllCatalogProductsBySearchTerm(catalogSearchRequest.SearchTerm);
            }
            else
            {
                if (catalogSearchRequest.CategoryId.HasValue && catalogSearchRequest.CategoryId.Value != 0)
                {
                    model.CatalogProducts = await this.catalogService.GetCatalogProductsByCategoryIdAsync(catalogSearchRequest.CategoryId.Value);
                }
                else
                {
                    model.CatalogProducts = await this.catalogService.GetAllCatalogProductsAsync();
                }
            }

            model.Categories = await this.categoryService.GetAllAsync();

            return View(model);
        }
    }
}
