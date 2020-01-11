using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Models
{
    public class Freelancer : IdentityUser
    {

      
        public User User { get; set; }
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string Skills { get; set; }
        
        [Required]
        public short WorkHoursPerWeek { get; set; }

    }
}
