using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models.ShoppingCart;
using Rentx.Web.Services.Interfaces;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rentx.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToCartViewModel model)
        {
            await this.shoppingCartService.AddAsync(model);
            return Ok();
        }
    }
}
