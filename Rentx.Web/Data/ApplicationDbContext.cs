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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

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

            builder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order);

            builder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails);

            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products);

            base.OnModelCreating(builder);
        }
    }
}
