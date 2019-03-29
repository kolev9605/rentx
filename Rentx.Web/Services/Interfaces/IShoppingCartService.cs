using Rentx.Web.Models;
using Rentx.Web.Models.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartViewModel> GetShoppingCartAsync(string userId);

        Task<ErrorViewModel> AddAsync(AddToCartViewModel model);

        Task<ErrorViewModel> RemoveAsync(int shoppingCartDetailsId);

        Task<ErrorViewModel> UpdateAsync(ShoppingCartViewModel shoppingCartViewModel);
    }
}
