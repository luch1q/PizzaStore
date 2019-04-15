using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PizzaStore.Models;

namespace PizzaStore.Components
{
    public class NavigationAdminMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
