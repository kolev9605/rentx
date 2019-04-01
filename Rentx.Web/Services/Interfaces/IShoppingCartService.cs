using Rentx.Web.Models;
using Rentx.Web.Models.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartViewModel> GetShoppingCartAsync(string userId);

        Task<MessageViewModel> AddAsync(AddToCartViewModel model);

        Task<MessageViewModel> RemoveAsync(int shoppingCartDetailsId);

        Task<MessageViewModel> UpdateAsync(ShoppingCartViewModel shoppingCartViewModel);
    }
}
