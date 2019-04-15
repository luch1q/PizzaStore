using Microsoft.AspNetCore.Mvc;
using PizzaStore.Infrastructure;
using PizzaStore.Models;
namespace PizzaStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;
        public CartSummaryViewComponent()
        {
        }
        public IViewComponentResult Invoke()
        {
            return View(cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart());
        }
    }
}
