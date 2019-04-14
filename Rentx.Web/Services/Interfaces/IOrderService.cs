using Rentx.Web.Models;
using Rentx.Web.Models.Admin;
using Rentx.Web.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAllOrderDetailsAsync();
        
        Task<OrderDetailsViewModel> GetOrderDetailsAsync(int shoppingCartId);

        Task<MessageViewModel> SubmitOrderAsync(OrderDetailsViewModel model, string userId);

        Task<MessageViewModel> ConfirmOrderAsync(int orderId);
    }
}
