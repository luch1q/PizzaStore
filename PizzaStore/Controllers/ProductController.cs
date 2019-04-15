using Microsoft.AspNetCore.Mvc;
using PizzaStore.Models;
using System.Linq;
using PizzaStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 3;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        public ViewResult List(string category, int productPage = 1) 
            => View(new ProductsListViewModel {
            Products = _repository.Products
                .Where(p => category == null && p.IsCustom == false || p.Category.Name == category && p.IsCustom == false)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize)
                .Include(p => p.Category),
            PagingInfo = new PagingInfo {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                _repository.Products.Where(e => e.IsCustom == false).Count():
                _repository.Products.Where(e =>
                e.Category.Name == category && e.IsCustom == false).Count()
            },
            CurrentCategory = category,
            ProductIngredients = _repository.ProductIngredients
                .Include(p => p.Ingredient)
                .ToList()
            });
    }
}