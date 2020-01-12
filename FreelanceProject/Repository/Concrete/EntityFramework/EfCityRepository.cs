﻿using FreelanceProject.Entity;
using FreelanceProject.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Repository.Concrete.EntityFramework
{
    public class EfCityRepository : EfGenericRepository<City>, ICityRepository
    {


        public EfCityRepository(ProjectContext context) : base(context)
        {

        }

        public ProjectContext ProjectContext { get { return context as ProjectContext; } }

    }
}
