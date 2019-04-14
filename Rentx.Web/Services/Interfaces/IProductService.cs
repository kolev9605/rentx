using Rentx.Web.Models.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task AddAsync(ProductViewModel model);

        Task<bool> DeleteByIdAsync(int productId);
        
        Task<IEnumerable<ProductViewModel>> GetAllAsync();

        Task<ProductViewModel> GetByIdAsync(int productId);

        Task<bool> UpdateAsync(ProductViewModel model);
    }
}