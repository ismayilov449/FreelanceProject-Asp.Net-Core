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


        public IActionResult SearchJobs(SearchJobModel searchJobModel)
        {
            var jobs = new List<Job>();
            string notnull = "";
            int educationvalue = 0;
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
                        notnull += "Education/";
                        educationvalue = uow.Education.Find(i => i.EducationName == searchJobModel.Education).FirstOrDefault().EducationValue;

                        var educations = uow.Education.Find(i => i.EducationValue >= educationvalue).ToList();

                        foreach (var item in educations)
                        {
                            currentjobs = currentjobs.Where(i => i.Education == item.EducationName);
                        }
                    }
                }
                if (!String.IsNullOrWhiteSpace(searchJobModel.Experience))
                {
                    if (searchJobModel.Experience != "All")
                    {
                        notnull += "Experience/";
                        experiencevalue = uow.Experience.Find(i => i.ExperienceName == searchJobModel.Experience).FirstOrDefault().ExperienceValue;

                        var experiences = uow.Experience.Find(i => i.ExperienceValue >= experiencevalue).ToList();

                        foreach (var item in experiences)
                        {
                            currentjobs = currentjobs.Where(i => i.Experience == item.ExperienceName);
                        }

                    }

                }
                if (searchJobModel.Salary != 0)
                {

                    notnull += "Salary";
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