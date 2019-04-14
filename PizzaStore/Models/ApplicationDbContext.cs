using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace PizzaStore.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ProductIngredient
            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductID, pi.IngredientID });

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(p => p.Product)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.ProductID);

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(p => p.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pc => pc.IngredientID);
            //bugfix
            modelBuilder.Entity<Order>().
                Property(o => o.DateTime)
                .HasColumnType("datetime2")
                .IsRequired();
            //ProductOrder
            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.ProductID, po.OrderID });
            modelBuilder.Entity<ProductOrder>()
                .HasOne(p => p.Product)
                .WithMany(o => o.ProductOrder)
                .HasForeignKey(po => po.ProductID);

            modelBuilder.Entity<ProductOrder>()
                .HasOne(p => p.Order)
                .WithMany(o => o.ProductOrder)
                .HasForeignKey(po => po.OrderID);
        }
    }
}
