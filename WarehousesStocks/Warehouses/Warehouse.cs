using System.Collections.Generic;
using System.Linq;

namespace WarehousesStocks.Warehouses
{
    public class Warehouse
    {
        public string Name { get; }
        public int Total => Products.Sum(x => x.Amount);
        public IEnumerable<WarehouseProduct> Products { get; private set; }

        internal Warehouse(string name, IEnumerable<WarehouseProduct> products)
        {
            Name = name;
            Products = products;
        }

        internal void AddProduct(WarehouseProduct product)
        {
            Products = Products.Append(product);
        }

        internal void SetProducts(IEnumerable<WarehouseProduct> products)
        {
            Products = products;
        }
    }
}