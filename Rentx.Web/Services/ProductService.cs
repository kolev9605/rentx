﻿using Microsoft.AspNetCore.Http.Internal;
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

        public async Task AddAsync(ProductViewModel model)
        {
            var product = new Product
            {
                Title = model.Title,
                Price = model.Price,
                Description = model.Description,
                AvailableQuantity = model.AvailableQuantity,
                Image = model.ImageFileName
            };

            await this.dbContext.Products.AddAsync(product);
            var result = await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(int productId)
        {
            var product = await this.GetProductById(productId);
            this.dbContext.Products.Remove(product);
            var result = await this.dbContext.SaveChangesAsync();

            var success = result > 0;
            return success;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
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

        public async Task<ProductViewModel> GetByIdAsync(int productId)
        {
            var product = await this.GetProductById(productId);

            var productModel = new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                AvailableQuantity = product.AvailableQuantity,
                ImageFileName = product.Image
            };

            return productModel;
        }

        public async Task<bool> UpdateAsync(ProductViewModel model)
        {
            var product = await this.GetProductById(model.Id);
            product.Price = model.Price;
            product.Title = model.Title;
            product.AvailableQuantity = model.AvailableQuantity;
            product.Description = model.Description;
            product.Image = model.ImageFileName;

            this.dbContext.Products.Update(product);
            var result = await this.dbContext.SaveChangesAsync();

            var success = result > 0;
            return success;
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