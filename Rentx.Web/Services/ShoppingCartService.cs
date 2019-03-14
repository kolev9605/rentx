using Rentx.Web.Data;
using Rentx.Web.Data.Entities;
using Rentx.Web.Models.ShoppingCart;
using Rentx.Web.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext dbContext;

        public ShoppingCartService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(AddToCartViewModel model)
        {
            var userCart = await this.GetOrCreateUserCartAsync(model.UserId);
            var productToAdd = this.dbContext.Products.FirstOrDefault(p => p.Id == model.ProductId);
            if (productToAdd == null)
            {
                throw new ArgumentNullException($"Product With id {model.ProductId} could not be found.");
            }

            ShoppingCartDetails shoppingCartDetails = new ShoppingCartDetails
            {
                ProductId = productToAdd.Id,
                Qantity = model.Quantity,
                ShoppingCartId = userCart.Id
            };

            userCart.ShoppingCartDetails.Add(shoppingCartDetails);
            await dbContext.SaveChangesAsync();
        }

        private async Task<ShoppingCart> GetOrCreateUserCartAsync(string userId)
        {
            ShoppingCart userCart = this.dbContext.ShoppingCarts.FirstOrDefault(sc => sc.UserId == userId);
            if (userCart == null)
            {
                ShoppingCart shoppingCart = new ShoppingCart()
                {
                    UserId = userId
                };

                this.dbContext.ShoppingCarts.Add(shoppingCart);
                await dbContext.SaveChangesAsync();

                return shoppingCart;
            }

            return userCart;
        }
    }
}