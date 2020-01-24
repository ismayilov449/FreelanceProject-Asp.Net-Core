using FreelanceProject.Entity;
using FreelanceProject.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Repository.Concrete.EntityFramework
{
    public class EfJobClientRepository : EfGenericRepository<JobClient>, IJobClientRepository
    {

        public EfJobClientRepository(ProjectContext context) : base(context)
        {

        }

        public ProjectContext ProjectContext { get { return context as ProjectContext; } }

    }
}
