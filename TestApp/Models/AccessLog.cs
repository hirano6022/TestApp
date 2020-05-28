using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class AccessLog : TableEntity
    {
        public DateTime LogDateTime { get; set; }
        public string LogMessage { get; set; }

        public AccessLog()
        {
            PartitionKey = nameof(AccessLog);
            RowKey = Guid.NewGuid().ToString();
            LogDateTime = DateTime.UtcNow;
        }

    }
}