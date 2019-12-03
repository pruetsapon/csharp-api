using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Accounting.WS.Models;
using Accounting.WS.Models.DB;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Accounting.WS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : Controller
    {
        private readonly SystemDbContext _context;
        private readonly string DAILY_PERIOD = "daily";
        private readonly string MONTHLY_PERIOD = "monthly";
        private readonly string YEARLY_PERIOD = "yearly";

        public ReportController(SystemDbContext context)
        {
            _context = context;
        }

        // GET api/report
        [HttpGet]
        public IActionResult GetReport(
            [FromQuery] string date,
            [FromQuery] string period
        )
        {
            var qryDate = DateTime.Now;
            if(date != null)
            {
                qryDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            IQueryable<Income> incomes = _context.Income;
            IQueryable<Expenditure> expenditures = _context.Expenditure;
            if(period == DAILY_PERIOD)
            {
                incomes = incomes.Where(s => s.FundedTime.Date == qryDate.Date);
                expenditures = expenditures.Where(s => s.FundedTime.Date == qryDate.Date);
            }
            else if(period == MONTHLY_PERIOD)
            {
                incomes = incomes.Where(s => s.FundedTime.Month == qryDate.Month && s.FundedTime.Year == qryDate.Year);
                expenditures = expenditures.Where(s => s.FundedTime.Month == qryDate.Month && s.FundedTime.Year == qryDate.Year);
            }
            else if(period == YEARLY_PERIOD)
            {
                incomes = incomes.Where(s => s.FundedTime.Year == qryDate.Year);
                expenditures = expenditures.Where(s => s.FundedTime.Year == qryDate.Year);
            }
            var report = new Report();
            report.QueryTime = qryDate;
            report.Period = period;
            report.Incomes = incomes.ToList();
            report.Expenditures = expenditures.ToList();
            report.TotalIncome = incomes.Sum(i => i.Amount);
            report.TotalExpenditure = expenditures.Sum(i => i.GetTotal());
            report.Total = report.TotalIncome - report.TotalExpenditure;
            return Ok(report);
        }
    }
}
