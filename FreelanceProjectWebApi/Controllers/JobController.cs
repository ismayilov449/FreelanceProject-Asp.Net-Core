using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceProject.Models;
using FreelanceProject.Repository.Abstract;
using FreelanceProject.Repository.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private IUnitOfWork uow;
        public JobController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var jobs = uow.Jobs.GetAll();

            return Ok(jobs);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = uow.Jobs.Get(id);
                if (product == null)
                {
                    return NotFound($"There is no product with this Id : {id}");
                }
                return Ok(product);

            }
            catch (Exception)
            {
            }
            return BadRequest();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Job job)
        {
            try
            {
                uow.Jobs.Add(job);
                uow.SaveChanges();

                return new StatusCodeResult(201);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(Job job)
        {
            try
            {
                uow.Jobs.Edit(job);
                uow.SaveChanges();
                return Ok(job);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.Jobs.Delete(uow.Jobs.Find(i=> i.Id == id).FirstOrDefault());
                uow.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

    }
}