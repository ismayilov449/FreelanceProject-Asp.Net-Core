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
        public async Task<IActionResult> CreateJob(JobClientModel jobClientModel)
        {
            var currentuser = await userManager.FindByNameAsync(jobClientModel.Client.UserName);



            var job = new Job()
            {
                Age = jobClientModel.Job.Age,
                City = jobClientModel.Job.City,
                Description = jobClientModel.Job.Description,
                Education = jobClientModel.Job.Education,
                Experience = jobClientModel.Job.Experience,
                JobCategory = jobClientModel.Job.JobCategory,
                Position = jobClientModel.Job.Position,
                Price = jobClientModel.Job.Price,
                RequiredSkills = jobClientModel.Job.RequiredSkills,
                Title = jobClientModel.Job.Title,
                Token = Guid.NewGuid(),
                SharedTime = DateTime.Now,
                Deadline = jobClientModel.Job.Deadline


            };

            job.Client = jobClientModel.Client;
            job.Client.Id = jobClientModel.Client.Id;
            job.Client.CompanyName = jobClientModel.Client.CompanyName;
            //jobClientModel.Client.User.UserName = currentuser.UserName;

            int cnt = uow.Users.Find(i => i.Id == currentuser.Id).Count();

            if (cnt == 0)
            {
                uow.Users.Add(currentuser);
            }


            uow.Jobs.Add(job);

            uow.SaveChanges();

            return RedirectToAction("SuccessfullyAdded");


        }



        public IActionResult SuccessfullyAdded()
        {


            return View();


        }



    }
}