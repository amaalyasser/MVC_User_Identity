using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Identity.Models;

namespace Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = model.UserName;
                user.PasswordHash = model.Password;
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                   
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Instructor");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Message", item.Code);
                    }
                }
            }
            return View(model);
        }
         [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? applicationUser = await userManager.FindByNameAsync(model.UserName);
                if (applicationUser != null)
                {
                    bool isFound = await userManager.CheckPasswordAsync(applicationUser, model.Password);
                    if (isFound)
                    {
                        await signInManager.SignInAsync(applicationUser, model.RememberMe);
                        return RedirectToAction("Index", "Instructor");
                    }
                }
                else
                {
                    ModelState.AddModelError("Message", "UserName or Password is in correct");
                }

            }
            return View(model);
        }
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

       

    }
}
