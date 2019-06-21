using Rentx.Web.Models.Catalog;
using Rentx.Web.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogProductViewModel>> GetCatalogProductsByCategoryIdAsync(int categoryId, OrderType orderType);

        Task<IEnumerable<CatalogProductViewModel>> GetAllCatalogProductsAsync(OrderType orderType);

        Task<IEnumerable<CatalogProductViewModel>> GetAllCatalogProductsBySearchTerm(string searchTerm, OrderType orderType);
    }
}