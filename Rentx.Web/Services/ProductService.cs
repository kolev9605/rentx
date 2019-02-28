using Microsoft.EntityFrameworkCore;
using Rentx.Web.Data;
using Rentx.Web.Data.Entities;
using Rentx.Web.Models.Admin;
using Rentx.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> DeleteProductByIdAsync(int productId)
        {
            var product = await this.GetProductById(productId);
            this.dbContext.Products.Remove(product);
            var result = await this.dbContext.SaveChangesAsync();
            var success = result > 0;

            return success;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var products = await this.dbContext.Products
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    AvailableQuantity = p.AvailableQuantity
                })
                .ToListAsync();

            return products;
        }

        private async Task<Product> GetProductById(int productId)
        {
            var product = await this.dbContext.Products
                .FindAsync(productId);

            if (product == null)
            {
                throw new ArgumentNullException($"Product with id {productId} does not exist.");
            }

            return product;
        }
    }
}
