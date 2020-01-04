using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        IFreelancerRepository Freelancers { get; }
        IJobRepository Jobs { get; }

        IJobClientRepository JobClient { get; }

        int SaveChanges();
    }
}
