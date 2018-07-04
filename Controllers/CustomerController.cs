using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Webapi.DB.Model;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly SystemDbContext _context;

        public CustomerController(SystemDbContext context)
        {
            _context = context;
        }

        // GET api/customer
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _context.Customer;
            return Ok(customers);
        }

        // GET api/customer/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _context.Customer
                            .SingleOrDefault(c => c.Id == id);
            if(customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }

        // POST api/customer
        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            if(ModelState.IsValid)
            {
                customer.Created = DateTime.Now;
                _context.Customer.Add(customer);
                _context.SaveChanges();
                return Ok(customer);
            }
            return BadRequest();
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Customer customerModel)
        {
            var customer = _context.Customer
                            .SingleOrDefault(c => c.Id == id);
            if(customer != null)
            {
                customer.Name = customerModel.Name;
                customer.Lastname = customerModel.Lastname;
                customer.Updated = DateTime.Now;
                _context.SaveChanges();
                return Ok(customer);
            }
            return NotFound();
        }

        // DELETE api/customer/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customer
                            .SingleOrDefault(c => c.Id == id);
            if(customer != null)
            {
                _context.Customer.Remove(customer);
                _context.SaveChanges();
                return Ok(customer);
            }
            return NotFound();
        }
    }
}
