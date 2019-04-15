using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace PizzaStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue,
           ErrorMessage = "Недопустимая цена")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Укажите категорию")]
        public int CategoryID { get; set; }
        public string Photo { get; set; }
        [Range(0.01, double.MaxValue,
           ErrorMessage = "Недопустимый вес")]
        public decimal Weight { get; set; }
        public bool IsCustom { get; set; }

        public Product()
        {
            Photo = "1";
        }
 
        public Category Category { get; set; }
        public List<ProductIngredient> ProductIngredients { get; set; }
        public List<ProductOrder> ProductOrder { get; set; }
    }
}
