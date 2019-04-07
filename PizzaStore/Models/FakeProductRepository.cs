using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> {
            new Product {Name = "Pizza1", Price = 25},
            new Product {Name = "Pizza2", Price = 179},
            new Product {Name = "Pizza3", Price = 250}
        }.AsQueryable<Product>();
    }
}
