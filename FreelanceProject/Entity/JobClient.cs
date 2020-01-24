using FreelanceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Entity
{
    public class JobClient
    {

        public int Id { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }


        public int ClientId { get; set; }
        public Client Client { get; set; }

        public string Status { get; set; }



        public DateTime DateOfRequest { get; set; }

    }
}
