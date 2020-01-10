using FreelanceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Entity
{
    public class JobFreelancer
    {

        public int JobId { get; set; }
        public Job Job { get; set; }


        public int FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; }

        public string Status { get; set; }

        public DateTime DateOfRequest { get; set; }
    }
}
