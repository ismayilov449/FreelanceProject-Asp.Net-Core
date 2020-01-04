using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using FreelanceProject.Repository.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProject.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        // private EfUnitOfWork uow;
        private UserManager<User> userManager;
        private JobClient currentjob;
        private IUnitOfWork uow;

        public ClientController(UserManager<User> _userManager, IUnitOfWork _uow)
        {
            uow = _uow;
            userManager = _userManager;
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
            var currentuser = await userManager.FindByNameAsync(jobClientModel.Client.UserName);

            if (currentuser != null)
            {
                jobClientModel.Client.User = currentuser;
            }
            currentjob = new JobClient()
            {

                Client = jobClientModel.Client,


                Job = jobClientModel.Job,

            };

            if(!uow.Clients.Find(i=> i.Id != jobClientModel.Client.Id).Any())
            {
                uow.Clients.Add(currentjob.Client);
            }

            
            uow.Jobs.Add(currentjob.Job);
          
            uow.SaveChanges();

                return RedirectToAction("SuccessfullyAdded");
            
             
        }



        public IActionResult SuccessfullyAdded()
        {

           
            return View();

            
        }

       

    }
}