using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Entity;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProject.Controllers
{
    [Authorize]
    public class JobController : Controller
    {

        private IUnitOfWork uow;
        private IQueryable<Job> jobs;
        private static int currentJobId;
        public JobController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Details(int jobId)
        {

            currentJobId =jobId;
             jobs = uow.Jobs.Find(i => i.Id == jobId);


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


        [Authorize(Roles="Freelancer")]
        public IActionResult SendRequest(int Id)
        {
            jobs = uow.Jobs.Find(i => i.Id == Id);


            jobs = jobs
                    .Include(i => i.Client)
                    .Include(i => i.JobCategory);

            var temp = new Job();
            temp = jobs.FirstOrDefault();
            temp.Client = jobs.FirstOrDefault().Client;

            return View(temp);
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer")]
        public IActionResult SendRequest(Job job)
        {
            var temp = new JobFreelancer();

           // temp.Freelancer = uow.Users.Find(i => i.Id == job.Client.Id);

             
            return View();
        }
    }
}