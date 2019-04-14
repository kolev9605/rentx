using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rentx.Web.Common.Exceptions;
using Rentx.Web.Data;
using Rentx.Web.Data.Entities;
using Rentx.Web.Models;
using Rentx.Web.Models.Admin;
using Rentx.Web.Services.Interfaces;

namespace Rentx.Web.Services
{
    /// <summary>
    /// Service responsible for database operations on categories
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Adds category in the database
        /// </summary>
        /// <param name="categoryViewModel">Model used to create the category</param>
        /// <returns></returns>
        public async Task AddAsync(CategoryViewModel categoryViewModel)
        {
            var category = new Category
            {
                Name = categoryViewModel.Name
            };

            await this.dbContext.Categories.AddAsync(category);
            var result = await this.dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete category from the database by Id
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <returns>Message model with error message in case of error or success message.</returns>
        public async Task<MessageViewModel> DeleteByIdAsync(int categoryId)
        {
            var message = new MessageViewModel();
            var category = await this.GetCategoryById(categoryId);
            if (category.Products.Any())
            {
                message.SetError($"Категория с ID {categoryId} понеже е свързана с продукти.");
            }

            this.dbContext.Categories.Remove(category);
            var result = await this.dbContext.SaveChangesAsync();
            
            return message;
        }

        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>Collection of categories</returns>
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

        /// <summary>
        /// Returns category by category Id
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns>Category</returns>
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

        /// <summary>
        /// Updates the category by the given model
        /// </summary>
        /// <param name="categoryViewModel">Model used to update the category</param>
        /// <returns>Boolean value that indicates the success of the update</returns>
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
