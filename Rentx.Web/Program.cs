using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rentx.Web.Data;
using Rentx.Web.Data.Seeders;
using System;
using System.IO;

namespace Rentx.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ApplicationDbContext>();
                AdminSeeder.SeedDatabase(context, services).Wait();
                CategorySeeder.SeedDatabase(context).Wait();
                ProductSeeder.SeedDatabase(context).Wait();
            }

            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
