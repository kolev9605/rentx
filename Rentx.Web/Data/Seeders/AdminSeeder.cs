using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Rentx.Web.Common;
using Rentx.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Data.Seeders
{
    public class AdminSeeder
    {
        public static async Task SeedDatabase(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { RoleConstants.Administrator, RoleConstants.User };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            ApplicationUser user = await UserManager.FindByEmailAsync("admin@rentx.com");

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "admin@rentx.com",
                    Email = "admin@rentx.com",
                    ShoppingCartId = null,
                };

                await UserManager.CreateAsync(user, "admin@rentx.com");
            }

            await UserManager.AddToRoleAsync(user, RoleConstants.Administrator);

            context.SaveChanges();
        }
    }
}
