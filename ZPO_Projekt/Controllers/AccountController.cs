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