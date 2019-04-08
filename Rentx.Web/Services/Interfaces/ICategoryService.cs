using Rentx.Web.Models;
using Rentx.Web.Models.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface ICategoryService
    {
        Task AddAsync(CategoryViewModel categoryViewModel);

        Task<MessageViewModel> DeleteByIdAsync(int categoryId);

        Task<IEnumerable<CategoryViewModel>> GetAllAsync();

        Task<CategoryViewModel> GetByIdAsync(int categoryId);

        Task<bool> UpdateAsync(CategoryViewModel categoryViewModel);
    }
}
