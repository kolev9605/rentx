using Rentx.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Rentx.Web.Data.Seeders
{
    public static class ProductSeeder
    {
        public static void SeedDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Products.Any())
            {
                context.Products.AddRange(GetCategoriesToSeed());

                context.SaveChanges();
            }
        }

        private static IEnumerable<Product> GetCategoriesToSeed()
        {
            var products = new List<Product>
            {
                new Product()
                {
                    Title = "Canon 1",
                    Description = "Mnogo hubav",
                    AvailableQuantity = 10,
                    Price = 100
                },
                new Product()
                {
                    Title = "Canon 2",
                    Description = "Oshte po hubav",
                    AvailableQuantity = 3,
                    Price = 300
                },
                new Product()
                {
                    Title = "Sony X",
                    Description = "Ako nqmash kinti",
                    AvailableQuantity = 32,
                    Price = 60
                }
            };

            return products;
        }
    }
}