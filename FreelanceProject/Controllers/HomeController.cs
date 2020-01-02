using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProject.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Freelancer")]
        public IActionResult IndexForFreelancer()
        {
            return View();
        }

        [Authorize(Roles = "Client")]
        public IActionResult IndexForClient()
        {
            return View();
        }



    }
}