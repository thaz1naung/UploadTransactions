using Dapper;
using Microsoft.Extensions.Logging;
using MyCoreMVCDemo.Entities;
using MyCoreMVCDemo.Models;
using MyCoreMVCDemo.Repositories.Bases;
using MyCoreMVCDemo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Repositories
{
    public class ImportRepository : BaseRepository, IImportRepository
    {
        private readonly ILogger<ImportRepository> _logger;
        private readonly IDBSetting _dbSetting;
        public ImportRepository(MyCoreDbContext coredbContext, ILogger<ImportRepository> logger, IDBSetting dbSetting) : base(coredbContext, dbSetting)
        {
            _logger = logger;
            _dbSetting = dbSetting;
        }

        public IEnumerable<DataTransactionViewModel> RetrieveDataByCurrency(string currency)
        {
            try
            {
                var dapperparam = new DynamicParameters();
                dapperparam.Add("@p_Currency", currency);              
                
                var sqlQuery = "RetrieveDataByCurrency";
                var result = GetSelectSPResult<DataTransactionViewModel>(dapperparam, sqlQuery);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in RetrieveDataByCurrency " + ex.Message);
                return null;
            }
        }

        public IEnumerable<DataTransactionViewModel> RetrieveDataByDateRange(string fromdate, string todate)
        {
            try
            {
                var dapperparam = new DynamicParameters();
                dapperparam.Add("@p_FromDate", Convert.ToDateTime(fromdate));
                dapperparam.Add("@p_ToDate", Convert.ToDateTime(todate));

                var sqlQuery = "RetrieveDataByDateRange";
                var result = GetSelectSPResult<DataTransactionViewModel>(dapperparam, sqlQuery);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in RetrieveDataByCurrency " + ex.Message);
                return null;
            }
        }

        public IEnumerable<DataTransactionViewModel> RetrieveDataByStatus(string status)
        {

            try
            {
                var dapperparam = new DynamicParameters();
                dapperparam.Add("@p_Status", status);

                var sqlQuery = "RetrieveDataByStatus";
                var result = GetSelectSPResult<DataTransactionViewModel>(dapperparam, sqlQuery);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in RetrieveDataByCurrency " + ex.Message);
                return null;
            }
        }

        public async Task<bool> SaveData(DataTransaction data)
        {
            _coredbContext.DataTransaction.Add(data);
            var result = await _coredbContext.SaveChangesAsync();
            return result == 1;
        }
       
        
    }
}
