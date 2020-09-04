using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eazywebapp_eindwerk.Models;
using Eazywebapp_eindwerk.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eazywebapp_eindwerk.Controllers
{
    
    public class AccountController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signinManager;

        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<User> signinManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signinManager = signinManager;
        }

        [Authorize(Roles = "User")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult Register(Register obj)
        {
            if (ModelState.IsValid)
            {
                if (!roleManager.RoleExistsAsync("User").Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "User";
                    IdentityResult roleResult =  roleManager.CreateAsync(role).Result;
                }

                User user = new User{};
                user.UserName = obj.Email;
                user.Email = obj.Email;
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.CompanyName = obj.CompanyName;
                user.VAT = obj.VAT;
                user.Address = obj.Address;
                user.City = obj.City;
                user.Country = obj.Country;
                user.PostalCode = obj.PostalCode;
                user.Logo = obj.Logo;

                IdentityResult result =  userManager.CreateAsync(user, obj.Password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", "Probleem met aanmaak gebruiker: " + error);
                    }
                }
            }
            return View(obj);
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInAsync(SignIn obj)
        {
            if (ModelState.IsValid)
            {
                var result = await signinManager.PasswordSignInAsync
                (obj.UserName, obj.Password,
                    obj.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Probleem bij inloggen gebruiker");
                }
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignOutAsync()
        {
            signinManager.SignOutAsync().Wait();
            return RedirectToAction("SignIn", "Account");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }


    }

}