using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Models
{
    public class DataViewModel
    {
        public int code { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = string.Empty;
   
    }
}
