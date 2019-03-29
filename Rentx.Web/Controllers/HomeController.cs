using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models;
using Rentx.Web.Models.Catalog;
using Rentx.Web.Services.Interfaces;

namespace Rentx.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService catalogService;
        private readonly ICategoryService categoryService;

        public HomeController(ICatalogService catalogService, ICategoryService categoryService)
        {
            this.catalogService = catalogService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var model = new CatalogViewModel();

            if (categoryId.HasValue && categoryId.Value != 0)
            {
                model.CatalogProducts = await this.catalogService.GetCatalogProductsByCategoryIdAsync(categoryId.Value);
            }
            else
            {
                model.CatalogProducts = await this.catalogService.GetAllCatalogProductsAsync();
            }
            
            model.Categories = await this.categoryService.GetAllAsync();

            return View(model);
        }
    }
}
