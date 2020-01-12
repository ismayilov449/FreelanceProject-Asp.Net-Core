using FreelanceProject.Entity;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Services
{
    static public class FakeRepo
    {
        static public IUnitOfWork uow { get; set; }
      

        static public List<JobCategory> GetCategories()
        {
            return uow.Categories.GetAll().ToList();
        }

        static public List<City> GetCities()
        {
            return uow.Cities.GetAll().ToList();
        }

        static public List<Education> GetEducation()
        {
            return uow.Education.GetAll().ToList();
        }

        static public List<Experience> GetExperiences()
        {
            return uow.Experience.GetAll().ToList();
        }

        static public List<Salary> GetSalary()
        {
            return uow.Salary.GetAll().ToList();
        }

    }
}
