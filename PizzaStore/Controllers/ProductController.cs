﻿using Microsoft.AspNetCore.Mvc;
using PizzaStore.Models;
using System.Linq;
using PizzaStore.Models.ViewModels;

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
                .Where(p => category == null || p.Category.Name == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                _repository.Products.Count():
                _repository.Products.Where(e =>
                e.Category.Name == category).Count()
            },
            CurrentCategory = category
            });
    }
}