using CsvHelper.Configuration.Attributes;
using System;

namespace SaleReport.BLL.FileParser
{
    public class CsvFileRecord
    {
        [Index(0)]
        public DateTime Date { get; set; }
        [Index(1)]
        public string ClientName { get; set; }
        [Index(2)]
        public string ProductName { get; set; }
        [Index(3)]
        public decimal Sum { get; set; }
    }
}
