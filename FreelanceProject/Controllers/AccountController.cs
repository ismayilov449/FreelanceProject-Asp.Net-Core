using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private RoleManager<IdentityRole> roleManager;

        private IPasswordValidator<User> passwordValidator;
        private IPasswordHasher<User> passwordHasher;

        public AccountController(RoleManager<IdentityRole> _roleManager, UserManager<User> _userManager, SignInManager<User> _signInManager, IPasswordValidator<User> _passwordValidator, IPasswordHasher<User> _passwordHasher)
        {
            roleManager = _roleManager;

            userManager = _userManager;
            signInManager = _signInManager;

            passwordValidator = _passwordValidator;
            passwordHasher = _passwordHasher;
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");

                    }

                }
                ModelState.AddModelError("Email", "Invalid Email Address");
            }


            return View(model);
        }


        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            Random random = new Random();


            if (ModelState.IsValid)
            {
                var tempusername = model.Name.Substring(0, 3) + model.Surname.Substring(1, 4) + "_" + random.Next(1, 100);
                var user = new User()
                {


                    UserName = tempusername.ToLower(),
                    Email = model.Email,
                    Surname = model.Surname

                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var currentUser = await userManager.FindByNameAsync(user.UserName);


                    var roleresult = await userManager.AddToRoleAsync(currentUser, model.Role.ToString());

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsFreelancer(RegisterModel model)
        {

            Random random = new Random();


            if (ModelState.IsValid)
            {
                var tempusername = model.Name.Substring(0, 3) + model.Surname.Substring(1, 4) + "_" + random.Next(1, 100);
                var user = new User()
                {


                    UserName = tempusername.ToLower(),
                    Email = model.Email,
                    Surname = model.Surname

                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var currentUser = await userManager.FindByNameAsync(user.UserName);


                    var roleresult = await userManager.AddToRoleAsync(currentUser, "Freelancer");

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsClient(RegisterModel model)
        {
            Random random = new Random();

            if (ModelState.IsValid)
            {
                var tempusername = model.Name.Substring(0, 3) + model.Surname.Substring(1, 4) + "_" + random.Next(1, 100);
                var user = new User()
                {


                    UserName = tempusername.ToLower(),
                    Email = model.Email,
                    Surname = model.Surname

                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var currentUser = await userManager.FindByNameAsync(user.UserName);


                    var roleresult = await userManager.AddToRoleAsync(currentUser, "Client");

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}