﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Models.ViewModels
{
    public class LegoViewModel
    {
        public IEnumerable<Ingredient> ListofIngredients { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
