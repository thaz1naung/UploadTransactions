using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCoreMVCDemo.Entities;
using MyCoreMVCDemo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyCoreMVCDemo.Controllers
{
    [Route("v1/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IImportService _importService;
        public TransactionController(IImportService importService)
        {

            _importService = importService;

        }

        [HttpGet]
        [Route("currency/{currencycode}")]
        public dynamic GetDataByCurrency(string currencycode)
        {
            try
            {
                var result = _importService.RetrieveDataByCurrency(currencycode);
                return new { code = HttpStatusCode.OK, success = "True", message = "Success", data = result };
            }
            catch (Exception ex)
            {
                return new { code = HttpStatusCode.InternalServerError, success = "False", message = "Fail", data = ex.Message };
            }
        }
        [HttpGet]
        [Route("status/{status}")]
        public dynamic GetDataByStatus(string status)
        {
            try
            {
                var result = _importService.RetrieveDataByStatus(status);
                return new { code = HttpStatusCode.OK, success = "True", message = "Success", data = result };
            }
            catch (Exception ex)
            {
                return new { code = HttpStatusCode.InternalServerError, success = "False", message = "Fail", data = ex.Message };
            }
        }
        [HttpGet]
        [Route("transactiondate")]
        public dynamic GetDataByDateRange([FromQuery] DataRequestDTO param)
        {
            try
            {
                var result = _importService.RetrieveDataByDateRange(param);
                return new { code = HttpStatusCode.OK, success = "True", message = "Success", data = result };
            }
            catch (Exception ex)
            {
                return new { code = HttpStatusCode.InternalServerError, success = "False", message = "Fail", data = ex.Message };
            }
        }
    }
}
