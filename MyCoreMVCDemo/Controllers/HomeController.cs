using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCoreMVCDemo.Models;
using MyCoreMVCDemo.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MyCoreMVCDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment Environment;
        private readonly IImportService _importService;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment _environment, IImportService importService)
        {
            _logger = logger;
            Environment = _environment;
            _importService = importService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormFile postedFiles)
        {
            DataViewModel result = new DataViewModel();
            try
            {
                if (!postedFiles.FileName.EndsWith(".csv") && !postedFiles.FileName.EndsWith(".xml"))
                {
                    ViewBag.ErrorMessage = "“Unknown format";
                    return View();
                }
                List<ImportViewModel> lsitData = new List<ImportViewModel>();
                if (postedFiles.FileName.EndsWith(".csv"))
                {
                    using (var sreader = new StreamReader(postedFiles.OpenReadStream()))
                    {

                        while (!sreader.EndOfStream)
                        {
                            string[] rows = sreader.ReadLine().Split(',');

                            lsitData.Add(new ImportViewModel
                            {
                                TransactionId = rows[0].ToString(),
                                Amount = rows[1].ToString(),
                                CurrencyCode = rows[2].ToString(),
                                TransactionDate = rows[3].ToString(),
                                Status = rows[4].ToString(),
                                FileType = "csv"
                            });
                        }
                    }

                }
                else if (postedFiles.FileName.EndsWith(".xml"))
                {

                    XmlDocument doc = new XmlDocument();
                    string str = null;
                    using (StreamReader streamReader = new StreamReader(postedFiles.OpenReadStream()))
                    {
                        str = streamReader.ReadToEnd();
                    }
                    doc.LoadXml(str);
                    //Loop through the selected Nodes.
                    foreach (XmlNode node in doc.SelectNodes("/Transactions/Transaction"))
                    {
                        //Fetch the Node values and assign it to Model.
                        ImportViewModel d = new ImportViewModel();
                        d.TransactionId = node.Attributes["id"].InnerText;
                        d.TransactionDate = node["TransactionDate"].InnerText;
                        d.Status = node["Status"].InnerText;
                        d.Amount = node.ChildNodes[1]["Amount"].InnerText;
                        d.CurrencyCode = node.ChildNodes[1]["CurrencyCode"].InnerText;
                        d.FileType = "xml";
                        lsitData.Add(d);
                    }


                }
                result = await _importService.ImportData(lsitData);
                if (!result.status)
                {
                    ViewBag.ErrorMessage = result.message;
                }
                else
                {
                    ViewBag.Message = result.message;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
