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
            FakeRepo.uow = _uow;
        }

        public IActionResult Index()
        {


            var jobs = uow.Jobs.Find(i=> i.IsApproved == true);


            jobs = jobs
                    .Include(i => i.Client);
             

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


        public IActionResult SearchJobs(SearchJobModel searchJobModel)
        {
            var jobs = new List<Job>();
            int experiencevalue = 0;

            var currentjobs = uow.Jobs.GetAll();

            if (searchJobModel != null)
            {
                if (!String.IsNullOrWhiteSpace(searchJobModel.Category))
                {
                    if (searchJobModel.Category != "All")
                    {
                        currentjobs = currentjobs.Where(i => i.Category == searchJobModel.Category);
                    }
                }
                if (!String.IsNullOrWhiteSpace(searchJobModel.City))
                {
                    if (searchJobModel.City != "All")
                    {
                        currentjobs = currentjobs.Where(i => i.City == searchJobModel.City);
                    }
                }
                if (!String.IsNullOrWhiteSpace(searchJobModel.Education))
                {
                    if (searchJobModel.Education != "All")
                    {

                        currentjobs = currentjobs.Where(i => i.Education == searchJobModel.Education);
                       
                    }
                }
                if (!String.IsNullOrWhiteSpace(searchJobModel.Experience))
                {
                    if (searchJobModel.Experience != "All")
                    {
                        experiencevalue = uow.Experience.Find(i => i.ExperienceName == searchJobModel.Experience).FirstOrDefault().ExperienceValue;

                        if (experiencevalue == 2)
                        {
                            currentjobs = currentjobs.Where(i => Int32.Parse(i.Experience) <= 1);
                        }
                        if(experiencevalue == 3)
                        {
                            currentjobs = currentjobs.Where(i => Int32.Parse(i.Experience) > 1 && Int32.Parse(i.Experience) <= 3);
                        }
                        if(experiencevalue == 4)
                        {
                            currentjobs = currentjobs.Where(i => Int32.Parse(i.Experience) > 3 && Int32.Parse(i.Experience) <= 5);

                        }
                        if(experiencevalue == 5)
                        {
                            currentjobs = currentjobs.Where(i =>  Int32.Parse(i.Experience) > 5);
                        }


                    }

                }
                if (searchJobModel.Salary != 0)
                {

                    currentjobs = currentjobs.Where(i => Int32.Parse(i.Price) >= searchJobModel.Salary);
                }

                jobs = currentjobs.Include(i => i.Client).ToList();



            }
            else
            {
                jobs = uow.Jobs.GetAll().Include(i => i.Client).ToList();
            }
            searchJobModel.Jobs = jobs;
            return View(searchJobModel);
        }

        //[HttpPost]
        //public IActionResult SearchJobs(SearchJobModel searchJobModel)
        //{



        //    return View();
        //}
    }
}