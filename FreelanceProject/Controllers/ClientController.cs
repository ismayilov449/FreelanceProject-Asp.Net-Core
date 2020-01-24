using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Entity;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using FreelanceProject.Repository.Concrete.EntityFramework;
using FreelanceProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            FakeRepo.uow = uow;
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
                Category = jobClientModel.Job.Category,
                Position = jobClientModel.Job.Position,
                Price = jobClientModel.Job.Price,
                RequiredSkills = jobClientModel.Job.RequiredSkills,
                Title = jobClientModel.Job.Title,
                Token = Guid.NewGuid(),
                SharedTime = DateTime.Now,
                Deadline = jobClientModel.Job.Deadline,
                IsPublished = true,
                IsApproved = false,
                FirstRequest = false


            };

            job.Client = jobClientModel.Client;
            job.Client.Id = jobClientModel.Client.Id;
            job.Client.StringId = currentuser.Id;

            job.Client.CompanyName = jobClientModel.Client.CompanyName;
            //jobClientModel.Client.User.UserName = currentuser.UserName;

            int cnt = uow.Users.Find(i => i.Id == currentuser.Id).Count();

            if (cnt == 0)
            {
                uow.Users.Add(currentuser);
            }

            uow.JobsClients.Add(new JobClient()
            {
                Client = job.Client,
                Job = job,
                DateOfRequest = DateTime.Now,
                Status = "Waiting"

            });

            uow.Jobs.Add(job);

            uow.SaveChanges();

            return RedirectToAction("SuccessfullyAdded");


        }

        public IActionResult SuccessfullyAdded()
        {


            return View();


        }

        public IActionResult LookJobs()
        {

            string currentuserid = HttpContext.Session.GetJson<string>("CurrentUserId");

            if (String.IsNullOrWhiteSpace(currentuserid))
            {
                using (StreamReader streamReader = new StreamReader("UserId.txt"))
                {
                    currentuserid = streamReader.ReadToEnd();
                }
            }

            var currentclients = uow.Clients.Find(i => i.StringId == currentuserid).ToList();

            var currentjobs = new List<Job>();
            foreach (var item in currentclients)
            {
                currentjobs.Add(uow.Jobs.Find(i => i.Client.Id == item.Id).First());
            }

            return View(currentjobs);
        }


        public IActionResult DeleteJob(Guid token)
        {
            var jobfordelete = uow.Jobs.Find(i => i.Token == token).FirstOrDefault();
            uow.Jobs.Find(i => i.Token == token).FirstOrDefault().IsPublished = false;

            uow.JobsFreelancers.Find(i => i.JobId == jobfordelete.Id).FirstOrDefault().Status = "Job was deleted";

            uow.SaveChanges();
            return RedirectToAction("LookJobs");

        }


        public IActionResult LookRequestsFromFreelancers(int jobId)
        {



            var currentFreelancers = uow.JobsFreelancers.Find(i => i.JobId == jobId).Include(i => i.Freelancer).ToList();

            var jobfreelancersmodel = new List<JobFreelancerModel>();
            var jobfreelancer = new JobFreelancerModel();

            foreach (var freelancer in currentFreelancers)
            {

                jobfreelancersmodel.Add(new JobFreelancerModel()
                {
                    CurrentFreelancer = uow.Freelancers.Find(i => i.Id == freelancer.Freelancer.Id).Include(i => i.User).FirstOrDefault(),
                    JobId = jobId
                });

            }



            return View(jobfreelancersmodel);
        }


        public IActionResult ApplyFreelancer(int jobId, string freelancerId)
        {

            uow.JobsFreelancers.Find(i => i.JobId == jobId && i.Freelancer.Id == freelancerId).FirstOrDefault().Status = "Applied";
            uow.SaveChanges();
            return RedirectToAction("LookJobs");
        }

        public IActionResult RejectFreelancer(int jobId, string freelancerId)
        {

            uow.JobsFreelancers.Find(i => i.JobId == jobId && i.Freelancer.Id == freelancerId).FirstOrDefault().Status = "Rejected";
            uow.SaveChanges();
            return RedirectToAction("LookJobs");
        }

    }
}