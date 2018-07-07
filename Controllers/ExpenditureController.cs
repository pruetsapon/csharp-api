using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Accounting.WS.Models.DB;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Accounting.WS.Controllers
{
    [Route("api/[controller]")]
    public class ExpenditureController : Controller
    {
        private readonly SystemDbContext _context;

        public ExpenditureController(SystemDbContext context)
        {
            _context = context;
        }

        // GET api/expenditure
        [HttpGet]
        public IActionResult Get([FromQuery] string date)
        {
            var filterDate = DateTime.Now;
            if(date != null)
            {
                filterDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            var expenditures = _context.Expenditure
                                .Include(e => e.ReFunds)
                                .Where(e => e.FundedTime.Date.Equals(filterDate.Date));
            return Ok(expenditures);
        }

        // GET api/expenditure/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var expenditure = _context.Expenditure
                                .Include(e => e.ReFunds)
                                .SingleOrDefault(e => e.Id == id);
            if(expenditure != null)
            {
                return Ok(expenditure);
            }
            return NotFound();
        }

        // POST api/expenditure
        [HttpPost]
        public IActionResult Create([FromBody] Expenditure expenditure)
        {
            if(ModelState.IsValid)
            {
                expenditure.Created = DateTime.Now;
                _context.Expenditure.Add(expenditure);
                _context.SaveChanges();
                return Ok(expenditure);
            }
            return BadRequest();
        }

        // PUT api/expenditure/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Expenditure model)
        {
            if(ModelState.IsValid)
            {
                var expenditure = _context.Expenditure
                                    .SingleOrDefault(e => e.Id == id);
                if(expenditure != null)
                {
                    expenditure.Remark = model.Remark;
                    expenditure.Amount = model.Amount;
                    expenditure.Updated = DateTime.Now;
                    _context.SaveChanges();
                    return Ok(expenditure);
                }
                return NotFound();
            }
            return BadRequest();
        }

        // DELETE api/expenditure/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var expenditure = _context.Expenditure
                                .Include(e => e.ReFunds)
                                .SingleOrDefault(e => e.Id == id);
            if(expenditure != null)
            {
                _context.Expenditure.Remove(expenditure);
                _context.SaveChanges();
                return Ok(expenditure);
            }
            return NotFound();
        }

        // POST api/expenditure/5/refund
        [HttpPost("{id}/refund")]
        public IActionResult CreateReFund(int id, [FromBody] ExpenditureReFund refund)
        {
            if(ModelState.IsValid)
            {
                var expenditure = _context.Expenditure
                                    .Include(e => e.ReFunds)
                                    .SingleOrDefault(e => e.Id == id);
                if(expenditure != null)
                {
                    refund.Created = DateTime.Now;
                    expenditure.ReFunds.Add(refund);
                    _context.SaveChanges();
                    return Ok(expenditure);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
