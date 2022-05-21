using MyCoreMVCDemo.Entities;
using MyCoreMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Services.Interface
{
    public interface IImportService
    {
        Task<DataViewModel> ImportData(List<ImportViewModel> data);
        IEnumerable<DataTransactionViewModel> RetrieveDataByCurrency(string currency);
        IEnumerable<DataTransactionViewModel> RetrieveDataByStatus(string status);
        IEnumerable<DataTransactionViewModel> RetrieveDataByDateRange(string fromdate, string todate);
    }
}
