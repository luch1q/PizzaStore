using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<Product> Products => _context.Products;
        public IQueryable<ProductIngredient> ProductIngredients => _context.ProductIngredients;
        public IQueryable<Category> Categories => _context.Categories;
        public IQueryable<Ingredient> Ingredients => _context.Ingredients;
    }
}
