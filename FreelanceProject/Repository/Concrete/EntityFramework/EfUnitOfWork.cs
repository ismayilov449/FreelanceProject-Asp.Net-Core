using FreelanceProject.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Repository.Concrete.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {

        private readonly ProjectContext projectContext;

        public EfUnitOfWork(ProjectContext _projectContext)
        {
            projectContext = _projectContext ?? throw new ArgumentNullException("dbcontext can not be null");
        }


        private IClientRepository _clients;

        private IFreelancerRepository _freelancers;

        private IJobRepository _jobs;

        private IUserRepository _users;

        private IJobFreelancerRepository _jobsFreelancers;

        private ICityRepository _cities;

        private IExperienceRepository _experience;

        private IEducationRepository _education;
        
        private IJobCategoryRepository _jobCategories;

        private ISalaryRepository _salary;




        public ISalaryRepository Salary
        {
            get
            {
                return _salary ?? (_salary = new EfSalaryRepository(projectContext));
            }
        }

        public IJobCategoryRepository Categories
        {
            get
            {
                return _jobCategories ?? (_jobCategories = new EfJobCategoryRepository(projectContext));
            }
        }

        public IEducationRepository Education
        {
            get
            {
                return _education ?? (_education = new EfEducationRepository(projectContext));
            }
        }

        public IExperienceRepository Experience
        {
            get
            {
                return _experience ?? (_experience = new EfExperienceRepository(projectContext));
            }
        }

        public ICityRepository Cities
        {
            get
            {
                return _cities ?? (_cities = new EfCityRepository(projectContext));
            }
        }

        public IJobFreelancerRepository JobsFreelancers
        {
            get
            {
                return _jobsFreelancers ?? (_jobsFreelancers = new EfJobFreelancerRepository(projectContext));
            }
        }

        public IUserRepository Users
        {
            get
            {
                return _users ?? (_users = new EfUserRepository(projectContext));
            }
        }

        public IClientRepository Clients
        {
            get
            {
                return _clients ?? (_clients = new EfClientRepository(projectContext));
            }
        }

        public IFreelancerRepository Freelancers
        {
            get
            {
                return _freelancers ?? (_freelancers = new EfFreelancerRepository(projectContext));
            }
        }

        public IJobRepository Jobs
        {
            get
            {
                return _jobs ?? (_jobs = new EfJobRepository(projectContext));
            }
        }

        
        public int SaveChanges()
        {
            try
            {
                return projectContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Dispose()
        {
            projectContext.Dispose();
        
        }
    }
}
