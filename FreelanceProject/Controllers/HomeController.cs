using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProject.Controllers
{
    
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("Freelancer"))
            {
                return RedirectToAction("Index","Freelancer");
            }
            else if(User.IsInRole("Client"))
            {
                return RedirectToAction("IndexForClient");
            }else
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            
        }

        [Authorize(Roles = "Freelancer")]
        public IActionResult IndexForFreelancer(Freelancer freelancer)
        {
            
            return View();
        }

        [Authorize(Roles = "Client")]
        public IActionResult IndexForClient(Client client)
        {
            return View();
        }



    }
}