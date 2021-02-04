using System;
using System.Globalization;

namespace SaleReportFiller
{
    class FileRecord
    {
        public DateTime Date { get; }
        public string Client { get; }
        public string Product { get; }
        public decimal Price { get; }

        public FileRecord(DateTime date, string client, string product, decimal price)
        {
            Date = date;
            Client = client;
            Product = product;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Date.ToString("dd.MM.yyyy")};{Client};{Product};{Price.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
