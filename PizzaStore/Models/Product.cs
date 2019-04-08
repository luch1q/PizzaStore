using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string Photo { get; set; }
        public decimal Weight { get; set; }
 
        public Category Category { get; set; }
        public List<ProductIngredient> ProductIngredients { get; set; }
    }
}
