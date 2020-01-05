using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Models
{
    public class Client : IdentityUser
    {
        public int Id { get; set; }

        public User User { get; set; }
        public string CompanyName { get; set; }

        public List<Job> Jobs { get; set; }
    }
}
