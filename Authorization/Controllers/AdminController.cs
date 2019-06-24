using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;

        public AdminController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserForm model)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser { Email = model.Email, UserName = model.Name };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var result = await userManager.DeleteAsync(user);

            if(result.Succeeded)
                return RedirectToAction("Index");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            ViewData["Id"] = Id;
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserForm model, string Id)
        {
            return Content(Id);
        }
    }
}