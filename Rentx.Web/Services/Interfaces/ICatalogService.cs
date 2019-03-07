using Rentx.Web.Models.Catalog;
using System.Collections.Generic;

namespace Rentx.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        IEnumerable<CatalogProductViewModel> GetCatalogProducts();
    }
}