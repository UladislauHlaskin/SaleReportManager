using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleReport.Model
{
    class SaleContext : SaleReportsContainer1
    {
        public bool ClientExists(Func<Client, bool> predicate)
        {
            return Clients.Any(predicate);
        }

        public Client GetClient(Func<Client, bool> predicate)
        {
            return Clients.FirstOrDefault(predicate);
        }

        public bool ProductExists(Func<Product, bool> predicate)
        {
            return Products.Any(predicate);
        }

        public Product GetProduct(Func<Product, bool> predicate)
        {
            return Products.FirstOrDefault(predicate);
        }

        public bool ManagerExists(Func<Manager, bool> predicate)
        {
            return Managers.Any(predicate);
        }

        public Manager GetManager(Func<Manager, bool> predicate)
        {
            return Managers.FirstOrDefault(predicate);
        }
    }
}
