using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Accounting.WS.Models.DB;
using Accounting.WS.Repositories;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Accounting.WS.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    public class ExpenditureController : Controller
    {
        private readonly IExpenditureRepository _expendRepository;
        private readonly IReFundRepository _refundRepository;
        public ExpenditureController(
            IReFundRepository refundRepository,
            IExpenditureRepository expendRepository)
        {
            _expendRepository = expendRepository;
            _refundRepository = refundRepository;
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
            var expenditures = _expendRepository.Get()
                                    .Include(e => e.ReFunds)
                                    .Where(e => e.FundedTime.Date.Equals(filterDate.Date));
            return Ok(expenditures);
        }

        // // GET api/expenditure/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var expenditure = _expendRepository.Get()
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
        public async Task<IActionResult> Create([FromBody] Expenditure expenditure)
        {
            if(ModelState.IsValid)
            {
                expenditure.Created = DateTime.Now;
                await _expendRepository.Create(expenditure);
                return Ok(expenditure);
            }
            return BadRequest();
        }

        // // PUT api/expenditure/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Expenditure model)
        {
            if(ModelState.IsValid)
            {
                var expenditure = _expendRepository.Get(id).Result;
                if(expenditure != null)
                {
                    expenditure.Remark = model.Remark;
                    expenditure.Amount = model.Amount;
                    expenditure.Updated = DateTime.Now;
                    await _expendRepository.Update(expenditure);
                    return Ok(expenditure);
                }
                return NotFound();
            }
            return BadRequest();
        }

        // // DELETE api/expenditure/5
        [HttpGet("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var expenditure = _expendRepository.Get(id).Result;
            if(expenditure != null)
            {
                await _expendRepository.Delete(id);
                return Ok(expenditure);
            }
            return NotFound();
        }

        // // POST api/expenditure/5/refund
        [HttpPost("{id}/refund")]
        public async Task<IActionResult> CreateReFund(int id, [FromBody] ExpenditureReFund refund)
        {
            if(ModelState.IsValid)
            {
                var expenditure = _expendRepository.Get(id).Result;
                if(expenditure != null)
                {
                    refund.ExpenditureId = expenditure.Id;
                    refund.Created = DateTime.Now;
                    await _refundRepository.Create(refund);
                    return Ok(expenditure);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
