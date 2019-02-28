using ZPO_Projekt.Data;
using ZPO_Projekt.Data.Entities;
using ZPO_Projekt.Identity;
using ZPO_Projekt.Models;
using ZPO_Projekt.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ZPO_Projekt.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationDbContext _context;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Seed()
        {
            Food food1 = new Food();
            food1.Id = Guid.NewGuid().ToString();
            food1.Name = "Pizza";
            food1.Price = 20;
            food1.IsChecked = false;
            food1.Type = DishType.MainDish;
            _context.Foods.Add(food1);
            await _context.SaveChangesAsync();

            Food food2 = new Food();
            food2.Id = Guid.NewGuid().ToString();
            food2.Name = "Chocolate Icecream";
            food2.Price = 5;
            food2.IsChecked = false;
            food2.Type = DishType.Desert;
            _context.Foods.Add(food2);
            await _context.SaveChangesAsync();

            Food food3 = new Food();
            food3.Id = Guid.NewGuid().ToString();
            food3.Name = "Tomato soup";
            food3.Price = 9;
            food3.IsChecked = false;
            food3.Type = DishType.Soup;
            _context.Foods.Add(food3);
            await _context.SaveChangesAsync();

            Food food4 = new Food();
            food4.Id = Guid.NewGuid().ToString();
            food4.Name = "Spaghetti";
            food4.Price = 15;
            food4.IsChecked = false;
            food4.Type = DishType.MainDish;
            _context.Foods.Add(food4);
            await _context.SaveChangesAsync();

            Food food5 = new Food();
            food5.Id = Guid.NewGuid().ToString();
            food5.Name = "Strawberry Icecream";
            food5.Price = 5;
            food5.IsChecked = false;
            food5.Type = DishType.Desert;
            _context.Foods.Add(food5);
            await _context.SaveChangesAsync();

            Food food6 = new Food();
            food6.Id = Guid.NewGuid().ToString();
            food6.Name = "Carrot soup";
            food6.Price = 8;
            food6.IsChecked = false;
            food6.Type = DishType.Soup;
            _context.Foods.Add(food6);
            await _context.SaveChangesAsync();

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.ReOrderMe, shouldLockout: false);

            if (result == SignInStatus.Success)
            {
                return RedirectToAction("Index", "Order");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
        
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Order");
                }
                AddErrors(result);
            }
            
            return View(model);
        }
        
        public ActionResult Settings()
        {
            var userId = User.Identity.GetUserId();

            var user = _context.Users.Select(x => new SettingsViewModel {
                Id = x.Id,
                Email = x.Email
            }).First(x => x.Id == userId);


            return View(user);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Settings(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = _context.Users.First(x => x.Id == userId);

                var passwordChange = await _userManager.ChangePasswordAsync(userId, model.Password, model.NewPassword);

                if (!passwordChange.Succeeded)
                {
                    ModelState.AddModelError("", "Old password is invalid");
                    return View(model);
                }

                await _userManager.SetEmailAsync(userId, model.Email);
                user.UserName = model.Email;

                await _context.SaveChangesAsync();
            }

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}