using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Extensions;
using Rentx.Web.Models.ShoppingCart;
using Rentx.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.shoppingCartService.GetShoppingCartAsync(this.User.GetUserId());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int productId)
        {
            AddToCartViewModel addToCartModel = new AddToCartViewModel()
            {
                ProductId = productId,
                Quantity = 1,
                UserId = this.User.GetUserId()
            };

            var errorViewModel = await this.shoppingCartService.AddAsync(addToCartModel);
            if (errorViewModel.HasError)
            {
                errorViewModel.SetErrorMessage(this);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int shoppingCartDetailsId)
        {
            var errorViewModel = await this.shoppingCartService.RemoveAsync(shoppingCartDetailsId);
            if (errorViewModel.HasError)
            {
                errorViewModel.SetErrorMessage(this);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ShoppingCartViewModel shoppingCartViewModel)
        {
            var errorViewModel = await this.shoppingCartService.UpdateAsync(shoppingCartViewModel);

            if (errorViewModel.HasError)
            {
                errorViewModel.SetErrorMessage(this);
            }

            return RedirectToAction("Index");
        }
    }
}
