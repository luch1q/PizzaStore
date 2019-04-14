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

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product dbEntry = _context.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.CategoryID = product.CategoryID;
                    dbEntry.Photo = product.Photo;
                    dbEntry.Weight = product.Weight;

                }
            }
            _context.SaveChanges();
        }

    }
}
