using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProject.Areas.Admin.Controllers
{
    //[Route("Admin/Home")]
    //[ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}