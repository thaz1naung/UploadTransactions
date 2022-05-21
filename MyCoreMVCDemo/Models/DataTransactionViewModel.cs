using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Models
{
    public class DataTransactionViewModel
    {
        public string Id { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
        
    }
}
