using Rentx.Tests.Helpers;
using Rentx.Web.Data.Entities;
using Rentx.Web.Models.Admin;
using Rentx.Web.Services;
using System.Linq;
using Xunit;

namespace Rentx.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async void GetAllAsync_ShouldReturnCorrectNumberOfProducts()
        {
            var context = DbContextHelper.GetContextWithData();
            ProductService service = new ProductService(context);

            int allProductsCount = context.Products.Count();
            var allProducts = await service.GetAllAsync();

            Assert.Equal(allProductsCount, allProducts.Count());
        }

        [Fact]
        public async void AddAsync_ShouldAddProductCorrectly()
        {
            var context = DbContextHelper.GetContextWithData();
            ProductService service = new ProductService(context);

            int allProductsCount = context.Products.Count();

            Category category = context.Categories.FirstOrDefault();
            ProductViewModel model = new ProductViewModel()
            {
                AvailableQuantity = 10,
                Category = category.Id,
                Description = "some description",
                ImageFileName = null,
                Price = 40,
                RentPrice = null,
                Title = "title"
            };

            await service.AddAsync(model);

            var allProducts = await service.GetAllAsync();
            Assert.Equal(allProductsCount + 1, allProducts.Count());

            var addedProduct = context.Products.FirstOrDefault(p => p.Title == model.Title);
            Assert.NotNull(addedProduct);
        }

        [Fact]
        public async void Test()
        {
            var context = DbContextHelper.GetContextWithData();
            ProductService service = new ProductService(context);

            var allProducts = await service.GetAllAsync();

        }
    }
}
