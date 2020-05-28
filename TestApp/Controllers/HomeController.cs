using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using TestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;

// Git test 4

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TelemetryClient telemetry = new TelemetryClient();

        public ActionResult Index()
        {
            telemetry.TrackEvent("Index Requested");

            var conn = ConfigurationManager.AppSettings["Storage"];
            var account = CloudStorageAccount.Parse(conn);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("accesslog");
            table.CreateIfNotExists();
            var msg = new AccessLog() { LogMessage = $"{nameof(Index)} GET でアクセスしました " };
            var insert = TableOperation.Insert(msg);
            table.Execute(insert);
            return View();
        }

        public ActionResult About()
        {
            try
            {
                throw new NotImplementedException("Exception の実験");
                ViewBag.Message = "Your application description page.";
                return View();
            }
            catch(Exception ex)
            {
                telemetry.TrackException(ex);
                throw;
            }
            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}