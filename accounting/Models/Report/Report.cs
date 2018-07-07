using System;
using Accounting.WS.Models.DB;
using System.Collections.Generic;

namespace Accounting.WS.Models
{
    public class Report
    {
        public string Period { get; set; }
        public DateTime QueryTime { get; set; }
        public List<Income> Incomes { get; set; }
        public List<Expenditure> Expenditures { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenditure { get; set; }
        public decimal Total { get; set; }
    }
}