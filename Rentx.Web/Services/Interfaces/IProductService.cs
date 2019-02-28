using Rentx.Web.Models.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> DeleteProductByIdAsync(int productId);
        
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
    }
}
