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
            lego.AddItem(ingredient);
            SaveLego(lego);
            return RedirectToAction("Index");
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
            return View(new LegoViewModel { ListofIngredients = GetLego().Ingredients,
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