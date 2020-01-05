using FreelanceProject.Repository.Abstract;
using FreelanceProject.Repository.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Models
{
    public class Jobs
    {

        public IQueryable<Job> Jobss { get; set; }

    }
}
