using FreelanceProject.Entity;
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

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobFreelancer>()

                .HasKey(pk => new { pk.FreelancerId, pk.JobId });

            modelBuilder.Entity<JobFreelancer>()

               .Property(pk => pk.Status );


            modelBuilder.Entity<JobFreelancer>()

               .Property(pk => pk.DateOfRequest);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<JobFreelancer> JobsFreelancers { get; set; }

    }
}
