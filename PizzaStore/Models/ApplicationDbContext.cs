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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
