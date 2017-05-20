using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using StorageASP.Models;

namespace StorageASP.Controllers
{
    public class TablesController : Controller
    {
        // GET: Tables
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateTable()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
   CloudConfigurationManager.GetSetting("storage"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("TestTable");
            ViewBag.Success = table.CreateIfNotExists();
            ViewBag.TableName = table.Name;

            return View();
        }
        public ActionResult AddEntity()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
   CloudConfigurationManager.GetSetting("storage"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("TestTable");
            CustomerEntity customer1 = new CustomerEntity("Anna", "Nedovba");
            customer1.Email = "Nedovba@test.com";
            TableOperation insertOperation = TableOperation.Insert(customer1);
            TableResult result = table.Execute(insertOperation);
            ViewBag.TableName = table.Name;
            ViewBag.Result = result.HttpStatusCode;

            return View();
        }
    }
}