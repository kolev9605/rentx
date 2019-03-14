using Rentx.Web.Models.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogProductViewModel>> GetCatalogProductsAsync();
    }
}