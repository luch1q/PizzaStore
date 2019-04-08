using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStore.Models
{
    public class ProductIngredient
    {
        public int ProductID { get; set; }
        public int IngredientID { get; set; }
        public int ProductCount { get; set; }

        public Product Product { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
