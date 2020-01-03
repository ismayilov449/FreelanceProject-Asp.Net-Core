using FreelanceProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Repository.Concrete.EntityFramework
{
    public class ProjectContext : DbContext
    {

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {

        }


        public DbSet<Client> Clients { get; set; }

        public DbSet<Freelancer> Freelancers { get; set; }

        public DbSet<Job> Jobs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobClient>()
                .HasKey(pk => new { pk.JobId, pk.ClientId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
