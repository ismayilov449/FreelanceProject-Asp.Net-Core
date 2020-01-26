using FreelanceProject.Entity;
using FreelanceProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Repository.Concrete.EntityFramework
{
    public class ProjectContextApi : DbContext
    {

        public ProjectContextApi(DbContextOptions<ProjectContextApi> options)
            : base(options)
        {

        }


        public DbSet<Client> Clients { get; set; }

        public DbSet<Freelancer> Freelancers { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Education> Education { get; set; }

        public DbSet<Experience> Experience { get; set; }

        public DbSet<Salary> Salary { get; set; }

        public DbSet<JobCategory> JobCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobFreelancer>()

                .HasKey(pk => new { pk.Id});

            modelBuilder.Entity<JobFreelancer>()

              .Property(pk => pk.JobId);

            modelBuilder.Entity<JobFreelancer>()

              .Property(pk => pk.FreelancerId);

            modelBuilder.Entity<JobFreelancer>()

               .Property(pk => pk.Status );


            modelBuilder.Entity<JobFreelancer>()

               .Property(pk => pk.DateOfRequest);



            modelBuilder.Entity<JobClient>()

              .HasKey(pk => new { pk.Id });

            modelBuilder.Entity<JobClient>()

              .Property(pk => pk.JobId);

            modelBuilder.Entity<JobClient>()

              .Property(pk => pk.ClientId);

            modelBuilder.Entity<JobClient>()

               .Property(pk => pk.Status);


            modelBuilder.Entity<JobClient>()

               .Property(pk => pk.DateOfRequest);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<JobFreelancer> JobsFreelancers { get; set; }

    }
}
