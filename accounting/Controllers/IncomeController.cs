using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Accounting.WS.Models.DB;
using System.Globalization;

namespace Accounting.WS.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class IncomeController : Controller
    {
        private readonly SystemDbContext _context;

        public IncomeController(SystemDbContext context)
        {
            _context = context;
        }

        // GET api/income
        [HttpGet]
        public IActionResult Get([FromQuery] string date)
        {
            var filterDate = DateTime.Now;
            if(date != null)
            {
                filterDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            var incomes = _context.Income.Where(i => i.FundedTime.Date.Equals(filterDate.Date));
            return Ok(incomes);
        }

        // GET api/income/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var income = _context.Income.SingleOrDefault(i => i.Id == id);
            if(income != null)
            {
                return Ok(income);
            }
            return NotFound();
        }

        // POST api/income
        [HttpPost]
        public IActionResult Create([FromBody] Income income)
        {
            if(ModelState.IsValid)
            {
                income.Created = DateTime.Now;
                _context.Income.Add(income);
                _context.SaveChanges();
                return Ok(income);
            }
            return BadRequest();
        }

        // PUT api/income/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Income model)
        {
            if(ModelState.IsValid)
            {
                var income = _context.Income.SingleOrDefault(i => i.Id == id);
                if(income != null)
                {
                    income.Remark = model.Remark;
                    income.Amount = model.Amount;
                    income.Updated = DateTime.Now;
                    _context.SaveChanges();
                    return Ok(income);
                }
                return NotFound();
            }
            return BadRequest();
        }

        // DELETE api/income/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var income = _context.Income.SingleOrDefault(i => i.Id == id);
            if(income != null)
            {
                _context.Income.Remove(income);
                _context.SaveChanges();
                return Ok(income);
            }
            return NotFound();
        }
    }
}
