using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Infrastructure;
using PizzaStore.Models;
using PizzaStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderRepository orderRepository;
        public CartController(IProductRepository repo, IOrderRepository order)
        {
            repository = repo;
            orderRepository = order;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        [Authorize(Roles = "Admins")]
        public ViewResult List() =>
           View(new LastViewModel { Orders = orderRepository.Orders.ToList() , ProductIngredients = repository.ProductIngredients.Include( i => i.Ingredient).ToList()});

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                product.ProductIngredients = repository.ProductIngredients.Include(pi => pi.Ingredient).Where(pi => pi.ProductID == productId).ToList();
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
                Cart cart = GetCart();
                cart.RemoveLine(productId);
                SaveCart(cart);
            return RedirectToAction("Index", new { returnUrl });
        }
        [HttpPost]
        public RedirectToActionResult Checkout()
        {
            Cart cart = GetCart();
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста!");
            }
            if (ModelState.IsValid)
            {
                Order order = new Order
                {
                    DateTime = DateTime.Now
                };

                order.ProductOrder = cart.Lines.Select( p => new ProductOrder{
                    Product = p.Product,
                    Order = order,
                    Quantity = p.Quantity})
                    .ToList();

                orderRepository.SaveOrder(order);
                SaveCart(null);
                return RedirectToAction("Completed");
            }else

            return RedirectToAction("Index");
        }
        public ViewResult Completed()
        {
            Cart cart = GetCart();
            cart.Clear();
            return View();
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
