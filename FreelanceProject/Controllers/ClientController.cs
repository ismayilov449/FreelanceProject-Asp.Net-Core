using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Models;
using FreelanceProject.Repository.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProject.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private EfUnitOfWork uow;

        public ClientController(EfUnitOfWork _uow)
        {
            uow = _uow;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateJob()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(JobClient jobClientModel)
        {




            var currentJob = new JobClient()
            {
              
                Client = jobClientModel.Client,
                Job = jobClientModel.Job

            };



            return RedirectToAction("SuccessfullyAdded");
        }


        
        public IActionResult SuccessfullyAdded()
        {
            return View();
        }

    }
}