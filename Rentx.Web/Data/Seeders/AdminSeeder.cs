using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Administrator", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("admin@rentx.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@rentx.com",
                    Email = "admin@rentx.com",
                };

                await UserManager.CreateAsync(user, "admin@rentx.com");
            }

            await UserManager.AddToRoleAsync(user, "Administrator");

            context.SaveChanges();
        }
    }
}
