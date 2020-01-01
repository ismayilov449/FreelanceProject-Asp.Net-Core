using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Models
{
    public class User : IdentityUser
    {

        [Required]
        public string Surname { get; set; }
        public string Image { get; set; }

       
        public string Description { get; set; }

    }
}
