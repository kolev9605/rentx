﻿using System.Collections.Generic;
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

        public async Task<IEnumerable<CatalogProductViewModel>> GetCatalogProductsAsync()
        {
            var products = await this.dbContext.Products
                .Select(p => new CatalogProductViewModel
                {
                    Title = p.Title.TrimTitle(),
                    Description = p.Description.TrimDescription(),
                    Image = p.Image,
                    Price = p.Price,
                    Id = p.Id
                })
                .ToListAsync();

            return products;
        }
    }
}