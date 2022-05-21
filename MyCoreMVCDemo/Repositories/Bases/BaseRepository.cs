using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyCoreMVCDemo.Entities;
using MyCoreMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Repositories.Bases
{
    
    public class BaseRepository
    {
        protected readonly MyCoreDbContext _coredbContext;
        private readonly IDBSetting _dbSetting;
        public BaseRepository(MyCoreDbContext coredbContext, IDBSetting dbSetting)
        {
            _coredbContext = coredbContext;
            _dbSetting = dbSetting;
        }
        protected IEnumerable<T> GetSelectSPResult<T>(DynamicParameters param, string sqlQuery)
        {
            using (var connection = new SqlConnection(_dbSetting.DBConnection))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var result = connection.Query<T>(sqlQuery, param, commandType: CommandType.StoredProcedure);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                return result;
            }
        }
    }
}
