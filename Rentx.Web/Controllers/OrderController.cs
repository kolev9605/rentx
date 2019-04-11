using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Extensions;
using Rentx.Web.Models.Order;
using Rentx.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    /// <summary>
    /// Controller responsible for order operations
    /// </summary>
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IShoppingCartService shoppingCartService;

        public OrderController(IOrderService orderService, IShoppingCartService shoppingCartService)
        {
            this.orderService = orderService;
            this.shoppingCartService = shoppingCartService;
        }

        /// <summary>
        /// Returns view of the form used to submit order
        /// </summary>
        /// <param name="shoppingCartId">Shopping cart id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(int shoppingCartId)
        {
            OrderDetailsViewModel model = await this.orderService.GetOrderDetailsAsync(shoppingCartId);
            return View(model);
        }

        /// <summary>
        /// Posts the form and creates the order
        /// </summary>
        /// <param name="model">Model used to create the order</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(OrderDetailsViewModel model)
        {
            var messageViewModel = await this.orderService.SubmitOrderAsync(model, this.User.GetUserId());
            await this.shoppingCartService.ClearShoppingCart(this.User.GetUserId());
            
            messageViewModel.SetMessage(this);
            return RedirectToAction("Index", "Home");
        }


    }
}
