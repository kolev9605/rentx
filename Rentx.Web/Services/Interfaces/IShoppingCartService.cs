using Rentx.Web.Models.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartViewModel> GetShoppingCartAsync(string userId);

        Task AddAsync(AddToCartViewModel model);

        Task RemoveAsync(int shoppingCartDetailsId);

        Task UpdateAsync(IEnumerable<ShoppingCartItemViewModel> shoppingCartItems);
    }
}
