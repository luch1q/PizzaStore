using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models
{
    public class Lego
    {
        private List<LegoLine> lineIngredients = new List<LegoLine>();

        public virtual void AddItem(Ingredient ingredient, int quantity)
        {
            LegoLine line = lineIngredients
                .Where(p => p.Ingredient.IngredientID == ingredient.IngredientID)
                .FirstOrDefault();
            if (line == null)
                lineIngredients.Add(new LegoLine { Ingredient = ingredient, Quantity = quantity });
            else
                line.Quantity += quantity;
        }
        public virtual void RemoveItem(Ingredient ingredient)
        {
            lineIngredients.RemoveAll(i => i.Ingredient.IngredientID == ingredient.IngredientID);
        }
        public virtual List<LegoLine> Ingredients => lineIngredients;
    }

    public class LegoLine
    {
        public int LegoLineID { get; set; }
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
    }
}
