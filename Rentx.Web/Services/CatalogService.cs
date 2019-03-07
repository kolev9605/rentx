using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<CatalogProductViewModel> GetCatalogProducts()
        {
            var products = this.dbContext.Products
                .Select(p => new CatalogProductViewModel
                {
                    Title = p.Title,
                    Description = p.Description.Substring(0, 70).Trim() + "...",
                    Image = p.Image
                })
                .ToList();

            return products;
        }
    }
}