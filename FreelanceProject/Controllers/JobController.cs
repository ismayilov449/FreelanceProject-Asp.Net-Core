using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Entity;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using FreelanceProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProject.Controllers
{
    [Authorize]
    public class JobController : Controller
    {

        private IUnitOfWork uow;
        private IQueryable<Job> jobs;
        private UserManager<User> userManager;
        private static int currentJobId;
        public JobController(IUnitOfWork _uow, UserManager<User> _userManager)
        {
            uow = _uow;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Details(int jobId)
        {

            currentJobId = jobId;
            jobs = uow.Jobs.Find(i => i.Id == jobId);

            var currentjob = uow.Jobs.Find(i => i.Id == jobId).FirstOrDefault();

            var jobfreelancer = uow.JobsFreelancers.Find(i => i.Job.Id == jobId).Include(i => i.Freelancer).ToList();

            foreach (var item in jobfreelancer)
            {
                if(item.Freelancer.Id == HttpContext.Session.GetJson<string>("CurrentUserId"))
                {
                    foreach (var job in jobs)
                    {
                        job.FirstRequest = true;
                    }
                     
                }
            }
             


            jobs = jobs
                    .Include(i => i.Client)
                    .Include(i => i.JobCategory);


            //var count = products.Count();

            //products = products.Skip((page - 1) * PageSize).Take(PageSize);

            //return View(new ProductListModel()
            //{
            //    Products = products,
            //    PagingInfo = new PagingInfo()
            //    {
            //        CurrentPage = page,
            //        ItemPerPage = PageSize,
            //        TotalItems = count
            //    }

            //});


            return View(new Jobs()
            {
                Jobss = jobs

            });


        }


        [Authorize(Roles = "Freelancer")]
        public async Task<IActionResult> SendRequest(int Id)
        {
            //jobs = uow.Jobs.Find(i => i.Id == Id);


            //jobs = jobs
            //        .Include(i => i.Client)
            //        .Include(i => i.JobCategory);

            //var temp = new Job();
            //temp = jobs.FirstOrDefault();
            //temp.Client = jobs.FirstOrDefault().Client;

            var temp = new JobFreelancerModel();
            temp.CurrentFreelancer = new Freelancer();
            temp.CurrentFreelancer.User = new User();
            /*temp.CurrentFreelancer = */
            var curuser = await userManager.FindByNameAsync(User.Identity.Name);

            temp.CurrentFreelancer.Id = curuser.Id;
            temp.CurrentFreelancer.User.Name = curuser.Name;
            temp.CurrentFreelancer.User.Surname = curuser.Surname;
            temp.CurrentFreelancer.User.Email = curuser.Email;
            temp.CurrentFreelancer.User.Age = curuser.Age;

            temp.JobId = Id;


            return View(temp);
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer")]
        public IActionResult SendRequest(JobFreelancerModel jobFreelancerModel)
        {
            var temp = new JobFreelancer();

            temp.Freelancer = jobFreelancerModel.CurrentFreelancer;
            temp.Freelancer.Id = jobFreelancerModel.CurrentFreelancer.Id;
            temp.Freelancer.UserName = jobFreelancerModel.CurrentFreelancer.UserName;
            temp.Freelancer.User.Age = jobFreelancerModel.CurrentFreelancer.User.Age;
            temp.Freelancer.User.Name = jobFreelancerModel.CurrentFreelancer.User.Name;
            temp.Freelancer.User.Email = jobFreelancerModel.CurrentFreelancer.User.Email;
            temp.Freelancer.User.Image = jobFreelancerModel.CurrentFreelancer.User.Image;
            temp.Freelancer.User.UserName = jobFreelancerModel.CurrentFreelancer.UserName;
            temp.Freelancer.User.Surname = jobFreelancerModel.CurrentFreelancer.User.Surname;
            temp.DateOfRequest = DateTime.Now;
           
            temp.Job = uow.Jobs.Find(i => i.Id == jobFreelancerModel.JobId).First();
            
            if (uow.Freelancers.Find(i => i.Id ==  HttpContext.Session.GetJson<string>("CurrentUserId")).FirstOrDefault() == null)
            {
                uow.Freelancers.Add(temp.Freelancer);
            }


            temp.Status = "Waiting";


            uow.JobsFreelancers.Add(temp);
            uow.SaveChanges();


            return View();
        }
    }
}