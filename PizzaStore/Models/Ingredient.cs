using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
    }
}
