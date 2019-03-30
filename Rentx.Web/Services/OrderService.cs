using Rentx.Web.Common;
using Rentx.Web.Data;
using Rentx.Web.Models.Order;
using Rentx.Web.Services.Interfaces;
using System.Linq;

namespace Rentx.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public OrderDetailsViewModel GetOrderDetails(int shoppingCartId)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();

            var products = this.dbContext.ShoppingCartDetails
                .Where(scd => scd.ShoppingCartId == shoppingCartId)
                .Select(p => new OrderProductItemViewModel
                {
                    Title = p.Product.Title,
                    Price = p.Product.Price,
                    Quantity = p.Quantity,
                    Description = p.Product.Description.TrimDescription()
                })
                .ToList();

            model.Products = products;

            return model;
        }
    }
}
