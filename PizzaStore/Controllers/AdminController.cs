using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        
        #region Authorization 
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        public AdminController(UserManager<AppUser> usrMgr, IUserValidator<AppUser> userValid, 
            IPasswordValidator<AppUser> passValid, IPasswordHasher<AppUser> passwordHash, IProductRepository repo)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            repository = repo;
        }
        #endregion

        public ViewResult Index() => View(repository.Products.Where(p => !p.IsCustom));
        public ViewResult IndexAccount() => View(userManager.Users);

        public ViewResult Edit(int productId)
        {

            ViewBag.Category = 
                repository.Categories.Select( c => new SelectListItem() {
                    Value = c.CategoryID.ToString(),
                    Text = c.Name
                });
            return View(repository.Products.Include(a => a.Category)
                .FirstOrDefault(p => p.ProductID == productId));
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} был сохранён!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            ViewBag.Category =
                repository.Categories.Select(c => new SelectListItem()
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.Name
                });
            return View("Edit", new Product());
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} был удалён!";
            }
            return RedirectToAction("Index");

        }
        #region PostsFromAntoha
        public async Task<IActionResult> CreateAccount(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("Index", userManager.Users);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        public async Task<IActionResult> EditAccount(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditAccount(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }
        #endregion
    }
}