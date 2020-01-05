using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
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
    }
}