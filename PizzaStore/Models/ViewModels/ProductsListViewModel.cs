﻿using System.Collections.Generic;

namespace PizzaStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public IEnumerable<ProductIngredient> ProductIngredients { get; set; }
    }
}
