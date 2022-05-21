using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Entities
{
    public interface IDBSetting
    {
        string DBConnection { get; set; }
    }
    public class DBSetting : IDBSetting
    {
        public string DBConnection { get; set; }
    }
}
