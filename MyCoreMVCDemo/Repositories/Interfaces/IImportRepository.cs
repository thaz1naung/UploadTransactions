using MyCoreMVCDemo.Entities;
using MyCoreMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Repositories.Interfaces
{
    public interface IImportRepository
    {
        Task<bool> SaveData(DataTransaction data);
        IEnumerable<DataTransactionViewModel> RetrieveDataByCurrency(string currency);
        IEnumerable<DataTransactionViewModel> RetrieveDataByStatus(string status);
        IEnumerable<DataTransactionViewModel> RetrieveDataByDateRange(string fromdate, string todate);
    }
}
