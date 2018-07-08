using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.WS.Models.DB
{
    public class Income
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
    }
}