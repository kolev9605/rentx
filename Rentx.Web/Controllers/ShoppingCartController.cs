using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Extensions;
using Rentx.Web.Models.ShoppingCart;
using Rentx.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    /// <summary>
    /// Controller responsible for shopping cart operations
    /// </summary>
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        /// <summary>
        /// Returs view with the shopping cart of the current logged in user
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var model = await this.shoppingCartService.GetShoppingCartAsync(this.User.GetUserId());
            return View(model);
        }

        /// <summary>
        /// Adds product in the shopping cart
        /// </summary>
        /// <param name="productId">id of the product to add</param>
        /// <returns>Redurects to the shopping cart page</returns>
        [HttpGet]
        public async Task<IActionResult> Add(int productId)
        {
            AddToCartViewModel addToCartModel = new AddToCartViewModel()
            {
                ProductId = productId,
                Quantity = 1,
                UserId = this.User.GetUserId()
            };

            var messageViewModel = await this.shoppingCartService.AddAsync(addToCartModel);
            messageViewModel.SetMessage(this);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Removes product from the shopping cart
        /// </summary>
        /// <param name="shoppingCartDetailsId">Id of the shopping cart details to remove</param>
        /// <returns>Redurects to the shopping cart page</returns>
        [HttpGet]
        public async Task<IActionResult> Remove(int shoppingCartDetailsId)
        {
            var messageViewModel = await this.shoppingCartService.RemoveAsync(shoppingCartDetailsId);
            messageViewModel.SetMessage(this);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Updates the shopping cart by the given model
        /// </summary>
        /// <param name="shoppingCartViewModel">Model used to update the cart</param>
        /// <returns>Redurects to the shopping cart page</returns>
        [HttpPost]
        public async Task<IActionResult> Update(ShoppingCartViewModel shoppingCartViewModel)
        {
            var messageViewModel = await this.shoppingCartService.UpdateAsync(shoppingCartViewModel);
            messageViewModel.SetMessage(this);

            return RedirectToAction("Index");
        }
    }
}
