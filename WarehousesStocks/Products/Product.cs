using System.Collections.Generic;

namespace WarehousesStocks.Products
{
    public class Product
    {
        public string Id { get; }
        public string Name { get; }
        public IEnumerable<ProductWarehouse> Warehouses { get; }

        internal Product(string id, string name, IEnumerable<ProductWarehouse> warehouses)
        {
            Id = id;
            Name = name;
            Warehouses = warehouses;
        }
    }
}