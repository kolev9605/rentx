using Rentx.Web.Models.Order;

namespace Rentx.Web.Services.Interfaces
{
    public interface IOrderService
    {
        OrderDetailsViewModel GetOrderDetails(int shoppingCartId);
    }
}
