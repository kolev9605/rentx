using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rentx.Web.Common.Exceptions;
using Rentx.Web.Data;
using Rentx.Web.Data.Entities;
using Rentx.Web.Models.Admin;
using Rentx.Web.Services.Interfaces;

namespace Rentx.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(CategoryViewModel categoryViewModel)
        {
            var category = new Category
            {
                Name = categoryViewModel.Name
            };

            await this.dbContext.Categories.AddAsync(category);
            var result = await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(int categoryId)
        {
            var category = await this.GetCategoryById(categoryId);
            if (category.Products.Any())
            {
                throw new RentxValidationException($"Category with Id {categoryId} can't be deleted, because there are products associated with it.");
            }

            this.dbContext.Categories.Remove(category);
            var result = await this.dbContext.SaveChangesAsync();

            var success = result > 0;
            return success;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            var categories = await this.dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return categories;
        }

        public async Task<CategoryViewModel> GetByIdAsync(int categoryId)
        {
            var category = await this.GetCategoryById(categoryId);

            var categoryModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return categoryModel;
        }

        public async Task<bool> UpdateAsync(CategoryViewModel categoryViewModel)
        {
            var category = await this.GetCategoryById(categoryViewModel.Id);
            category.Name = categoryViewModel.Name;

            this.dbContext.Categories.Update(category);
            var result = await this.dbContext.SaveChangesAsync();

            var success = result > 0;
            return success;
        }

        private async Task<Category> GetCategoryById(int categoryId)
        {
            var category = await this.dbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                throw new RentxValidationException($"Category with id {categoryId} does not exist.");
            }

            return category;
        }
    }
}
