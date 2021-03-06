﻿using Rentx.Web.Models.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentx.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogProductViewModel>> GetCatalogProductsByCategoryIdAsync(int categoryId);

        Task<IEnumerable<CatalogProductViewModel>> GetAllCatalogProductsAsync();

        Task<IEnumerable<CatalogProductViewModel>> GetAllCatalogProductsBySearchTerm(string searchTerm);
    }
}