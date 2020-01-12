using FreelanceProject.Entity;
using FreelanceProject.Repository.Abstract;
using FreelanceProject.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceProject.Models
{
    public class JobClientModel
    {
        private IUnitOfWork uow;

        public JobClientModel()
        {
            
        }

        public int Id { get; set; }
        public Client Client { get; set; }
        public Job Job { get; set; }

        
    }
}
