using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rentx.Web.Common;
using Rentx.Web.Data;
using Rentx.Web.Models.Catalog;
using Rentx.Web.Models.Enums;
using Rentx.Web.Services.Interfaces;

namespace Rentx.Web.Services
{
    /// <summary>
    /// Service responsible for database operations on catalog items
    /// </summary>
    public class CatalogService : ICatalogService
    {
        private readonly ApplicationDbContext dbContext;

        public CatalogService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Returns catalog products by Category Id
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <returns>Collection of products found</returns>
        public async Task<IEnumerable<CatalogProductViewModel>> GetCatalogProductsByCategoryIdAsync(int categoryId, OrderType orderType)
        {
            var productsQuery = this.dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new CatalogProductViewModel
                {
                    Title = p.Title.TrimTitle(),
                    Description = p.Description.TrimDescription(),
                    Image = p.Image,
                    Price = p.Price,
                    RentPrice = p.RentPrice,
                    Id = p.Id
                });

            if (orderType == OrderType.Ascending)
            {
                productsQuery = productsQuery.OrderBy(x => x.Price);
            }
            else if (orderType == OrderType.Descending)
            {
                productsQuery = productsQuery.OrderByDescending(x => x.Price);
            }

            var products = await productsQuery.ToListAsync();

            return products;
        }

        /// <summary>
        /// Returns all catalog products 
        /// </summary>
        /// <returns>Collection of products</returns>
        public async Task<IEnumerable<CatalogProductViewModel>> GetAllCatalogProductsAsync(OrderType orderType)
        {
            var productsQuery = this.dbContext.Products
                .Select(p => new CatalogProductViewModel
                {
                    Title = p.Title.TrimTitle(),
                    Description = p.Description.TrimDescription(),
                    Image = p.Image,
                    Price = p.Price,
                    RentPrice = p.RentPrice,
                    Id = p.Id
                });

            if (orderType == OrderType.Ascending)
            {
                productsQuery = productsQuery.OrderBy(x => x.Price);
            }
            else if (orderType == OrderType.Descending)
            {
                productsQuery = productsQuery.OrderByDescending(x => x.Price);
            }

            var products = await productsQuery.ToListAsync();

            return products;
        }

        /// <summary>
        /// Returns catalog products by keyword (the search checks for match only in the title of the product)
        /// </summary>
        /// <param name="searchTerm">Keyword used in the search</param>
        /// <returns>Collection of products found</returns>
        public async Task<IEnumerable<CatalogProductViewModel>> GetAllCatalogProductsBySearchTerm(string searchTerm, OrderType orderType)
        {
            var productsQuery = this.dbContext.Products
                .Where(p => p.Title.IndexOf(searchTerm.Trim(), System.StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(p => new CatalogProductViewModel
                {
                    Title = p.Title.TrimTitle(),
                    Description = p.Description.TrimDescription(),
                    Image = p.Image,
                    Price = p.Price,
                    RentPrice = p.RentPrice,
                    Id = p.Id
                });

            if (orderType == OrderType.Ascending)
            {
                productsQuery = productsQuery.OrderBy(x => x.Price);
            }
            else if (orderType == OrderType.Descending)
            {
                productsQuery = productsQuery.OrderByDescending(x => x.Price);
            }

            var products = await productsQuery.ToListAsync();

            return products;
        }
    }
}