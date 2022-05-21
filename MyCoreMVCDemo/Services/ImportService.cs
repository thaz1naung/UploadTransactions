using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyCoreMVCDemo.Entities;
using MyCoreMVCDemo.Models;
using MyCoreMVCDemo.Repositories.Interfaces;
using MyCoreMVCDemo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Services
{
    public class ImportService : IImportService
    {
        private readonly ILogger<ImportService> _logger;
        private readonly IImportRepository _importRepository;
        public ImportService(ILogger<ImportService> logger, IImportRepository importRepository)
        {
            _logger = logger;
            _importRepository = importRepository;
        }
        public async Task<DataViewModel> ImportData(List<ImportViewModel> listData)
        {
            string tmpString = string.Empty;  
            DataTable dtError = new DataTable();
            dtError.Columns.AddRange(new DataColumn[1] { new DataColumn("Error", typeof(string)) });


            foreach (ImportViewModel data in listData)
            {
                try
                {
                    int errno = 0;

                    errno = CheckValidation(data);
                    if (errno == 0)
                    {
                        DataTransaction d = new DataTransaction();
                        d.TransGUID = new Guid();
                        d.TransactionId = data.TransactionId;
                        d.TransactionDate =Convert.ToDateTime(data.TransactionDate);
                        d.Amount = Convert.ToDecimal(data.Amount);
                        d.CurrencyCode = data.CurrencyCode;
                        d.Status = data.Status;

                      var result= await _importRepository.SaveData(d);        
                    }
                    else
                    {
                       DataRow errdr= dtError.NewRow();

                        if (errno == 1)
                            tmpString += data.TransactionId + " - All data are mandatory.<br>";
                        else if (errno == 2)
                            tmpString += data.TransactionId  + " - exceed 50 characters.<br>";
                        else if (errno == 3)
                            tmpString += data.TransactionDate  + " - invalid date/time format.<br>";
                        else if (errno == 4)
                            tmpString += data.Amount + " - invalid number.<br>";
                        else if (errno == 5)
                            tmpString += data.CurrencyCode + " - invalid curreny code format.<br>";
                        else if (errno == 6)
                            tmpString += data.Status + " - invalid status.<br>";                  
                    }
                }
                catch (Exception ex)
                {
                    return new DataViewModel()
                    {
                        status = false,
                        code = 400,
                        message = ex.Message
                    };       
                }
            }
            if (string.IsNullOrEmpty(tmpString))
            {
                return new DataViewModel()
                {
                    status = true,
                    code = 200,
                    message = "Data has been processed"
                };
            }
            else
            {
                return new DataViewModel()
                {
                    status = false,
                    code = 400,
                    message = tmpString
                };
            }
        }
        public int CheckValidation(ImportViewModel data)
        {
            int result = 0;
            decimal number;
            if (string.IsNullOrEmpty(data.TransactionId) || string.IsNullOrEmpty(data.TransactionDate.ToString()) || string.IsNullOrEmpty(data.Amount.ToString()) || string.IsNullOrEmpty(data.CurrencyCode) || string.IsNullOrEmpty(data.Status))
                result = 1;
            else if (data.TransactionId.Length > 50)
                result = 2;
            else if (!ValidateDateTime(data.TransactionDate.ToString(),data.FileType))
                result = 3;          
            else if (!decimal.TryParse(data.Amount.ToString(), out number))
                result = 4;
            else if (!ValidateCurrencyCode(data.CurrencyCode))
                result = 5;
            else
                if(data.FileType == "csv")
                    if (data.Status.ToLower() != "approved" && data.Status.ToLower() != "failed" && data.Status.ToLower() != "finished")
                        result = 6;
                else
                     if (data.Status.ToLower() != "approved" && data.Status.ToLower() != "rejected" && data.Status.ToLower() != "done")
                        result = 6;

            return result;
        }
        private bool ValidateDateTime(string data,string type)
        {
            try
            {
                if(type == "csv")
                {
                    Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

                    //Verify whether date entered in dd/MM/yyyy format.
                    bool isValid = regex.IsMatch(data.Trim());

                    //Verify whether entered date is Valid date.
                    DateTime dt;
                    isValid = DateTime.TryParseExact(data, "dd/MM/yyyy hh:mm:ss", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
                    if (!isValid)
                    {
                        return false;
                    }
                }else
                {
                    DateTime temp;
                    if (!DateTime.TryParse(data ,out temp))
                    {
                        return false;
                    }
                   
                }
                //string[] temp = data.Split(' ');
                //string[] dateParts = temp[0].Split('/');
                //string[] timeParts = temp[1].Split(':');
                //DateTime testDate = new
                //    DateTime(Convert.ToInt32(dateParts[2]),
                //    Convert.ToInt32(dateParts[1]),
                //    Convert.ToInt32(dateParts[0]));

                //if (timeParts.Length < 2 || Convert.ToInt32(timeParts[0]) > 24 || Convert.ToInt32(timeParts[1]) >= 60)
                //    return false;

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        private bool ValidateCurrencyCode(string data)
        {
            bool isvalid = false;
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo ci in cultures)
            {
                RegionInfo ri = new RegionInfo(ci.LCID);
                if (ri.ISOCurrencySymbol == data)
                {
                    isvalid = true;
                    break;
                }
            }

            return isvalid;
        }

        public IEnumerable<DataTransactionViewModel> RetrieveDataByCurrency(string currency)
        {
            return _importRepository.RetrieveDataByCurrency(currency);
        }

        public IEnumerable<DataTransactionViewModel> RetrieveDataByStatus(string status)
        {
            return _importRepository.RetrieveDataByStatus(status);
        }

        public IEnumerable<DataTransactionViewModel> RetrieveDataByDateRange(string fromdate, string todate)
        {
            return _importRepository.RetrieveDataByDateRange(fromdate,todate);
        }
    }
}
