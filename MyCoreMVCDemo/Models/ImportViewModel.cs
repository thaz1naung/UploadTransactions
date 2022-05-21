using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Models
{
    public class ImportViewModel
    {
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string CurrencyCode { get; set; }
        public string TransactionDate { get; set; }
        public string FileType { get; set; }
    }
}
