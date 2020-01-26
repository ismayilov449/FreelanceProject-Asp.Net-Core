using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        //IClientRepository Clients { get; }
        //IFreelancerRepository Freelancers { get; }
        IJobRepository Jobs { get; }
        //IUserRepository Users { get; }
        //IJobCategoryRepository Categories { get; }
        //ICityRepository Cities { get; }
        //IExperienceRepository Experience { get; }
        //IEducationRepository Education { get; }
        //ISalaryRepository Salary { get; }
        //IJobFreelancerRepository JobsFreelancers { get; }
        //IJobClientRepository JobsClients { get; }


        int SaveChanges();
    }
}
