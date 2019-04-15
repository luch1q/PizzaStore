using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models.ViewModels
{
    public class LastViewModel
    {
        public List<Order> Orders { get; set; }
        public List<ProductIngredient> ProductIngredients {get; set;}
    }
}
