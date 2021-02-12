using System;
using SaleReport.Model;

namespace SaleReport.BLL.FileParser
{
    class LineParser
    {
        public static Record Parse(string line, SaleContext context)
        {
            var data = line.Split(';');
            if (data.Length < 4)
                return null;
            DateTime date;
            decimal sum;
            bool noErrors = DateTime.TryParse(data[0], out date);
            noErrors = (noErrors & decimal.TryParse(data[3], out sum));
            string clientName = data[1];
            string productName = data[2];

            if (!noErrors)
            {
                return null;
            }
            else
            {
                Record record = new Record();
                Client client;
                Product product;

                if (!context.ClientExists(c => c.Name == clientName))
                {
                    client = new Client() { Name = clientName };
                    context.Clients.Add(client);
                }
                else
                {
                    client = context.GetClient(c => c.Name == clientName);
                }

                if (!context.ProductExists(p => p.Name == productName))
                {
                    product = new Product() { Name = productName };
                    context.Products.Add(product);
                }
                else
                {
                    product = context.GetProduct(p => p.Name == productName);
                }

                record.Date = date;
                record.Sum = sum;
                record.Client = client;
                record.Product = product;
                return record;
            }
        }
    }
}
