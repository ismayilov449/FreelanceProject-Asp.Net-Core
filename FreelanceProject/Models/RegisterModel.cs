using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Models
{
    public class RegisterModel
    {


        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }
       
        [Required]
        public string Password { get; set; }

        [Required]
        public IdentityRole Role { get; set; }

       
        public string Address { get; set; }

        
        public string Skills { get; set; }

       
        public short WorkHoursPerWeek { get; set; }

        public string CompanyName { get; set; }

    }
}
