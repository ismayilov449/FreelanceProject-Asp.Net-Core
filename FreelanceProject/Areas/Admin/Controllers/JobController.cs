using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Entity;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProject.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class JobController : Controller
    {
        private IUnitOfWork uow;
        private IQueryable<Job> jobs;
        private UserManager<User> userManager;
       
        public JobController(IUnitOfWork _uow, UserManager<User> _userManager)
        {
            uow = _uow;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult LookJobs()
        {

            var jobs = uow.Jobs.Find(i => i.IsApproved == false && i.IsPublished == true);

            jobs = jobs
                  .Include(i => i.Client);


            return View(new Jobs()
            {
                Jobss = jobs

            }); ;

            
        }

        public IActionResult Details(int jobId)
        {

             
            jobs = uow.Jobs.Find(i => i.Id == jobId);
             
            jobs = jobs
                    .Include(i => i.Client);
             

            return View(new Jobs()
            {
                Jobss = jobs

            });


        }



        public IActionResult ApplyJob(int jobId)
        {

            var currentjob = uow.Jobs.Find(i => i.Id == jobId).FirstOrDefault();
            currentjob.IsApproved = true;
            currentjob.IsPublished = true;
            uow.Jobs.Edit(currentjob);
            uow.SaveChanges();
            return RedirectToAction("LookJobs");
        }


        public IActionResult RejectJob(int jobId)
        {
            var currentjob = uow.Jobs.Find(i => i.Id == jobId).FirstOrDefault();
            currentjob.IsApproved = false;
            currentjob.IsPublished = false;
            uow.Jobs.Edit(currentjob);
            uow.SaveChanges();
            return RedirectToAction("LookJobs");
        }

        

    }
}