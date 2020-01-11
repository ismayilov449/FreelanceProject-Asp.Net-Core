using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using FreelanceProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProject.Controllers
{
    [Authorize(Roles = "Freelancer")]
    public class FreelancerController : Controller
    {
        private IUnitOfWork uow;
        public FreelancerController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public IActionResult Index()
        {


            var jobs = uow.Jobs.GetAll();


            jobs = jobs
                    .Include(i => i.Client);


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

            }); ;
        }


        public IActionResult JobsForStatus(/*string status = "Waiting"*/)
        {
            var tempjobstatusmodel = new JobStatusModel();

            string currentuserid = HttpContext.Session.GetJson<string>("CurrentUserId");

            if (String.IsNullOrWhiteSpace(currentuserid))
            {
                using (StreamReader streamReader = new StreamReader("UserId.txt"))
                {
                    currentuserid = streamReader.ReadToEnd();
                }
            }

            var jobs = uow.JobsFreelancers.Find(i =>/* i.Status == status &&*/ i.Freelancer.Id == currentuserid)
                .Include(i => i.Job)
                .ThenInclude(i => i.Client)
                .Include(i => i.Freelancer).ToList();

            return View(jobs);
        }

    }
}