using Microsoft.EntityFrameworkCore;
using Rentx.Web.Data;
using Rentx.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentx.Tests.Helpers
{
    public static class DbContextHelper
    {
        public static ApplicationDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new ApplicationDbContext(options);

            var cameraCategory = new Category { Id = 1, Name = "Camera" };
            var tripodCategory = new Category { Id = 2, Name = "Tripod" };
            context.Categories.Add(cameraCategory);
            context.Categories.Add(tripodCategory);
            context.SaveChanges();

            for (int i = 0; i < 20; i++)
            {
                Random random = new Random();
                int randomint = random.Next(1, 50);
                var product = new Product()
                {
                    AvailableQuantity = randomint,
                    Title = $"product-{cameraCategory.Name}-{i}-{randomint}",
                    Category = cameraCategory,
                    Description = $"product-{cameraCategory.Name}-{i}-{randomint}-description",
                    Price = randomint * i,
                    RentPrice = (randomint + i) * 2,
                };

                context.Products.Add(product);
            }

            for (int i = 0; i < 20; i++)
            {
                Random random = new Random();
                int randomint = random.Next(1, 50);
                var product = new Product()
                {
                    AvailableQuantity = randomint,
                    Title = $"product-{tripodCategory.Name}-{i}-{randomint}",
                    Category = tripodCategory,
                    Description = $"product-{tripodCategory.Name}-{i}-{randomint}-description",
                    Price = randomint * i,
                    RentPrice = (randomint + i) * 2,
                };

                context.Products.Add(product);
            }

            context.SaveChanges();
            return context;
        }
    }
}
