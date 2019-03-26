using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models;
using Rentx.Web.Services.Interfaces;

namespace Rentx.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogService catalogService;

        public HomeController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await this.catalogService.GetCatalogProductsAsync();

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
