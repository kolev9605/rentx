using Microsoft.EntityFrameworkCore;
using Rentx.Web.Common;
using Rentx.Web.Data.Entities;
using System.Threading.Tasks;

namespace Rentx.Web.Data.Seeders
{
    public static class CategorySeeder
    {
        public static async Task SeedDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            string[] categoryNames = { CategoryConstants.CameraName, CategoryConstants.LensName, CategoryConstants.TripodName, CategoryConstants.RentName };

            foreach (var categoryName in categoryNames)
            {
                var categoryExist = await context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName) != null;
                if (!categoryExist)
                {
                    context.Categories.Add(new Category
                    {
                        Name = categoryName
                    });
                }
            }

            context.SaveChanges();
        }
    }
}