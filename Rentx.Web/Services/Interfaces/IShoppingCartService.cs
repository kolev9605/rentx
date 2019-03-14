using Rentx.Web.Models.ShoppingCart;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task AddAsync(AddToCartViewModel model);
    }
}
