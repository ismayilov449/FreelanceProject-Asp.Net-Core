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
            projectContext = _projectContext;
        }


        private IClientRepository _clients;

        private IFreelancerRepository _freelancers;

        private IJobRepository _jobs;

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
