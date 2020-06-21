using System.Collections.Generic;

namespace WarehousesStocks.Products
{
    public class Product
    {
        internal string Id { get; }
        internal string Name { get; }
        internal IEnumerable<ProductWarehouse> Warehouses { get; }

        internal Product(string id, string name, IEnumerable<ProductWarehouse> warehouses)
        {
            Id = id;
            Name = name;
            Warehouses = warehouses;
        }
    }
}