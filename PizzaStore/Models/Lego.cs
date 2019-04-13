using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models
{
    public class Lego
    {
        private List<Ingredient> ingredients = new List<Ingredient>();

        public virtual void AddItem(Ingredient ingredient)
        {
            ingredients.Add(ingredient);
        }
        public virtual void RemoveItem(Ingredient ingredient)
        {
            ingredients.RemoveAll(i => i.IngredientID == ingredient.IngredientID);
        }
        public virtual List<Ingredient> Ingredients => ingredients;
    }
}
