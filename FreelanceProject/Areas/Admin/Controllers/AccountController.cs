using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {


        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
          
        private RoleManager<IdentityRole> roleManager;
        private IUnitOfWork uow;


        public AccountController(RoleManager<IdentityRole> _roleManager, UserManager<User> _userManager, SignInManager<User> _signInManager, IUnitOfWork _uow)
        {

            roleManager = _roleManager;
            userManager = _userManager;
            signInManager = _signInManager;

            uow = _uow;

        }


        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Register()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            Random random = new Random();


            if (ModelState.IsValid)
            {
                var tempusername = model.Name.Substring(0, 3) + model.Surname.Substring(1, 4) + "_" + random.Next(1, 100);
                var user = new User()
                {

                    Name = model.Name,
                    UserName = tempusername.ToLower(),
                    Email = model.Email,
                    Surname = model.Surname


                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var currentUser = await userManager.FindByNameAsync(user.UserName);


                    await userManager.AddToRoleAsync(currentUser,"Admin");

                    await signInManager.SignInAsync(user, isPersistent: false);
                     
                    
                    return RedirectToAction("Index", "Home");
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

    }
}