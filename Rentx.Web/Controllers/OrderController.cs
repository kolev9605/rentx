using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models.Order;
using Rentx.Web.Services.Interfaces;

namespace Rentx.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult Index(int shoppingCartId)
        {
            OrderDetailsViewModel model = this.orderService.GetOrderDetails(shoppingCartId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(OrderDetailsViewModel model)
        {
            
            return RedirectToAction("Index", "Order");
        }
    }
}
