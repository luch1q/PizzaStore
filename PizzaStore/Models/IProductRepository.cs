using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<ProductIngredient> ProductIngredients { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Ingredient> Ingredients { get; }

        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }
}
