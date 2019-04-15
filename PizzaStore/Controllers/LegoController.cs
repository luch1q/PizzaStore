using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Infrastructure;
using PizzaStore.Models;
using PizzaStore.Models.ViewModels;

namespace PizzaStore.Controllers
{
    public class LegoController : Controller
    {
        private IProductRepository repository;

        public LegoController(IProductRepository repo)
        {
            repository = repo;
        }
        public RedirectToActionResult AddItem(int ingredientID)
        {
            Ingredient ingredient = repository.Ingredients.FirstOrDefault( i => i.IngredientID == ingredientID);
            Lego lego = GetLego();
            lego.AddItem(ingredient, 1);
            SaveLego(lego);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public RedirectToActionResult AddToCart()
        {
            Lego lego = GetLego();
            
            if (lego.Ingredients.Count() == 0)
            {
                ModelState.AddModelError("", "Добавьте ингредиентов!");
            }
            if (ModelState.IsValid)
            {
                Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
                Product product = new Product { Name = "CustomPizza", IsCustom = true, CategoryID = 1 };

                product.ProductIngredients = lego.Ingredients.Select(p =>
                   new ProductIngredient
                   {
                       Product = product,
                       Ingredient = p.Ingredient
                   }).ToList();
                product.Price = lego.Ingredients.Sum(s => s.Ingredient.Price) * 120;

                cart.AddItem(product, 1);

                HttpContext.Session.SetJson("Cart", cart);
                return RedirectToAction("Completed");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ViewResult Completed()
        {
            HttpContext.Session.SetJson("Lego", null);
            return View();
        }
        public RedirectToActionResult RemoveItem(int ingredientID)
        {
            Ingredient ingredient = repository.Ingredients.FirstOrDefault(i => i.IngredientID == ingredientID);
            Lego lego = GetLego();
            lego.RemoveItem(ingredient);
            SaveLego(lego);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View(new LegoViewModel { LegoLines = GetLego().Ingredients,
                Ingredients = repository.Ingredients }
            );
        }
        private Lego GetLego()
        {
            Lego lego = HttpContext.Session.GetJson<Lego>("Lego") ?? new Lego();
            return lego;
        }
        private void SaveLego(Lego lego)
        {
            HttpContext.Session.SetJson("Lego", lego);
        }
    }
}