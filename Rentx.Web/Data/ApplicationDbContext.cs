using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rentx.Web.Data.Entities;

namespace Rentx.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartDetails> ShoppingCartDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShoppingCart>()
                .HasMany(sc => sc.ShoppingCartDetails)
                .WithOne(scd => scd.ShoppingCart);

            builder.Entity<ShoppingCartDetails>()
                .HasOne(scd => scd.Product)
                .WithMany(p => p.ShoppingCartDetails);

            builder.Entity<ShoppingCart>()
                .HasOne(sc => sc.User)
                .WithOne(u => u.ShoppingCart)
                .HasForeignKey<ApplicationUser>(u => u.ShoppingCartId);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products);

            base.OnModelCreating(builder);
        }
    }
}
