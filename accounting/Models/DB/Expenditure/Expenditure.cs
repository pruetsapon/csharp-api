using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.WS.Models.DB
{
    public class Expenditure
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Remark { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Column(TypeName="Timestamp")]
        public DateTime FundedTime { get; set; }
        public Nullable<DateTime> Created { get; set; }
        public Nullable<DateTime> Updated { get; set; }
        public List<ExpenditureReFund> ReFunds { get; set; }

        public decimal GetTotal()
        {
            var total = this.Amount;
            if(this.ReFunds != null && this.ReFunds.Count > 0)
            {
                total -= this.ReFunds.Sum(r => r.Amount);
            }
            return total;
        }
    }
}