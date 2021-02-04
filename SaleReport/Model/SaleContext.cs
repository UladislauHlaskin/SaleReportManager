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
            var exists = Clients.Any(predicate);
            if (!exists)
            {
                exists = Clients.Local.Any(predicate);
            }
            return exists;
        }

        public Client GetClient(Func<Client, bool> predicate)
        {
            var item = Clients.FirstOrDefault(predicate);
            if (item == null)
            {
                item = Clients.Local.FirstOrDefault(predicate);
            }
            return item;
        }

        public bool ProductExists(Func<Product, bool> predicate)
        {
            var exists = Products.Any(predicate);
            if (!exists)
            {
                exists = Products.Local.Any(predicate);
            }
            return exists;
        }

        public Product GetProduct(Func<Product, bool> predicate)
        {
            var item = Products.FirstOrDefault(predicate);
            if (item == null)
            {
                item = Products.Local.FirstOrDefault(predicate);
            }
            return item;
        }

        public bool ManagerExists(Func<Manager, bool> predicate)
        {
            var exists = Managers.Any(predicate);
            if (!exists)
            {
                exists = Managers.Local.Any(predicate);
            }
            return exists;
        }

        public Manager GetManager(Func<Manager, bool> predicate)
        {
            var item = Managers.FirstOrDefault(predicate);
            if (item == null)
            {
                item = Managers.Local.FirstOrDefault(predicate);
            }
            return item;
        }
    }
}
