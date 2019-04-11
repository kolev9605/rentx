using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Rentx.Web.Common.Exceptions;
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
    /// <summary>
    /// Service responsible for database operations on products
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Adds product in the database
        /// </summary>
        /// <param name="model">Model used to create the product</param>
        /// <returns></returns>
        public async Task AddAsync(ProductViewModel model)
        {
            var product = new Product
            {
                Title = model.Title,
                Price = model.Price,
                RentPrice = model.RentPrice,
                Description = model.Description,
                AvailableQuantity = model.AvailableQuantity,
                Image = model.ImageFileName,
                CategoryId = model.Category
            };

            await this.dbContext.Products.AddAsync(product);
            var result = await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes product from the database by Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Boolean value that indicates the success of the delete</returns>
        public async Task<bool> DeleteByIdAsync(int productId)
        {
            var product = await this.GetProductById(productId);
            this.dbContext.Products.Remove(product);
            var result = await this.dbContext.SaveChangesAsync();

            var success = result > 0;
            return success;
        }

        /// <summary>
        /// Returns all products from the database
        /// </summary>
        /// <returns>Collection of products</returns>
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await this.dbContext.Products
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    RentPrice = p.RentPrice,
                    Description = p.Description,
                    AvailableQuantity = p.AvailableQuantity
                })
                .ToListAsync();

            return products;
        }

        /// <summary>
        /// Returns product by given product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>ProductViewModel of the found product</returns>
        public async Task<ProductViewModel> GetByIdAsync(int productId)
        {
            var product = await this.GetProductById(productId);

            var productModel = new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                RentPrice = product.RentPrice,
                Description = product.Description,
                AvailableQuantity = product.AvailableQuantity,
                ImageFileName = product.Image,
                Category = product.Category.Id
            };

            return productModel;
        }

        /// <summary>
        /// Updates product by given model
        /// </summary>
        /// <param name="model">Model used to update the product</param>
        /// <returns>Boolean value that indicates the success of the update</returns>
        public async Task<bool> UpdateAsync(ProductViewModel model)
        {
            var product = await this.GetProductById(model.Id);
            product.Price = model.Price;
            product.RentPrice = model.RentPrice;
            product.Title = model.Title;
            product.AvailableQuantity = model.AvailableQuantity;
            product.Description = model.Description;
            product.Image = model.ImageFileName;
            product.CategoryId = model.Category;

            this.dbContext.Products.Update(product);
            var result = await this.dbContext.SaveChangesAsync();

            var success = result > 0;
            return success;
        }

        private async Task<Product> GetProductById(int productId)
        {
            var product = await this.dbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new RentxValidationException($"Product with id {productId} does not exist.");
            }

            return product;
        }
    }
}