using Rentx.Web.Models;
using Rentx.Web.Models.Order;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDetailsViewModel> GetOrderDetailsAsync(int shoppingCartId);

        Task<MessageViewModel> SubmitOrderAsync(OrderDetailsViewModel model, string userId);
    }
}
