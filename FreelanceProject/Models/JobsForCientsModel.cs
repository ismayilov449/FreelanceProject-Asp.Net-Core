using FreelanceProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Models
{
    public class JobsForCientsModel
    {

        public IQueryable<JobClient> Jobs { get; set; }

    }
}
