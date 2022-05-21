using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Entities
{
    public class DataTransaction
    {
        [Key]
        public Guid TransGUID { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
