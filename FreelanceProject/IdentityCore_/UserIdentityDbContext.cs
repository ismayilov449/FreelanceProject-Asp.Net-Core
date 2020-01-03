using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Models
{
    public class UserIdentityDbContext : IdentityDbContext<User>
    {

        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options)
            : base(options)
        {

        }

    }
}
