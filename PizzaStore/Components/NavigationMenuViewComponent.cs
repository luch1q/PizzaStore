using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PizzaStore.Models;
using PizzaStore.Models.ViewModels;

namespace PizzaStore.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke() {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
                return View(repository.Products
                    .Select(x => x.Category.Name)
                    .Distinct()
                    .OrderBy(x => x));
        }
    }
}
