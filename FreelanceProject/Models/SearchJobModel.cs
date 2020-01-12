using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Models
{
    public class SearchJobModel
    {

        public string Category { get; set; }

        public string City { get; set; }

        public string Experience { get; set; }

        public int Salary { get; set; }

        public string Education { get; set; }

        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
