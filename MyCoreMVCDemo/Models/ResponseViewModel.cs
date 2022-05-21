using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Models
{
    public class ResponseViewModel
    {
        public object data { get; set; }
        public bool error { get; set; } = false;
        public string errorMsg { get; set; } = string.Empty;
        public string errorCode { get; set; } = string.Empty;
        public int? errorID { get; set; } = 0;
    }
}
