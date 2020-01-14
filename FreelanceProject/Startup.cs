using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using FreelanceProject.Repository.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FreelanceProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserIdentityDbContext>(options =>
          options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsersDb;Trusted_Connection=True;MultipleActiveResultSets=true"));


            services.AddDbContext<ProjectContext>(options =>
        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FreelanceProjectDb;Trusted_Connection=True;MultipleActiveResultSets=true"));

            services.AddTransient<IJobRepository, EfJobRepository>();
            services.AddTransient<IClientRepository, EfClientRepository>();
            services.AddTransient<ICityRepository, EfCityRepository>();
            services.AddTransient<IEducationRepository, EfEducationRepository>();
            services.AddTransient<IExperienceRepository, EfExperienceRepository>();
            services.AddTransient<IJobCategoryRepository, EfJobCategoryRepository>();
            services.AddTransient<ISalaryRepository, EfSalaryRepository>();
            services.AddTransient<IFreelancerRepository, EfFreelancerRepository>();

            services.AddTransient<IUnitOfWork, EfUnitOfWork>();





            services.AddIdentity<User, IdentityRole>(options =>
            {

                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 7;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;



            })
                           .AddEntityFrameworkStores<UserIdentityDbContext>()
                           .AddDefaultTokenProviders();

           

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMemoryCache();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseStatusCodePages();
            app.UseAuthentication();


            app.UseMvc(routes =>
            {


                //routes.MapRoute(
                //name: "areas",
                //template: "{area:exists}/{controller=Home}/{action=Index}");

               

                routes.MapRoute(
                     name: "default",
                     template: "{controller}/{action}/{id?}",
                     defaults: new { controller = "Home", action = "Index" }
                     );
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller}/{action}/{id?}",
                     defaults: new { controller = "Home", action = "Index" }
                    );


            });


        }
    }
}
