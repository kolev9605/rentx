using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rentx.Web.Common;
using Rentx.Web.Data;
using Rentx.Web.Models.Catalog;
using Rentx.Web.Services.Interfaces;

namespace Rentx.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ApplicationDbContext dbContext;

        public CatalogService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CatalogProductViewModel>> GetCatalogProductsByCategoryIdAsync(int categoryId)
        {
            var products = await this.dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new CatalogProductViewModel
                {
                    Title = p.Title.TrimTitle(),
                    Description = p.Description.TrimDescription(),
                    Image = p.Image,
                    Price = p.Price,
                    RentPrice = p.RentPrice,
                    Id = p.Id
                })
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<CatalogProductViewModel>> GetAllCatalogProductsAsync()
        {
            var products = await this.dbContext.Products
                .Select(p => new CatalogProductViewModel
                {
                    Title = p.Title.TrimTitle(),
                    Description = p.Description.TrimDescription(),
                    Image = p.Image,
                    Price = p.Price,
                    RentPrice = p.RentPrice,
                    Id = p.Id
                })
                .ToListAsync();

            return products;
        }
    }
}