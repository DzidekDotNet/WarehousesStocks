using System.Collections.Generic;
using System.Linq;
using WarehousesStocks.Warehouses;

namespace WarehousesStocks.Tests.Warehouses
{
    internal class WarehouseEqualityComparer : IEqualityComparer<Warehouse>
    {
        public bool Equals(Warehouse actual, Warehouse expected)
        {
            if (actual == null && expected == null)
                return true;
            else if (expected == null || actual == null)
                return false;
            else if (expected.Name == actual.Name
                     && expected.Total == actual.Total)
                return actual.Products.Intersect(expected.Products, new WarehouseProductEqualityComparer()).Count() == expected.Products.Count();
            else
                return false;
        }

        public int GetHashCode(Warehouse obj)
        {
            long hCode = obj.Name.GetHashCode() ^ obj.Total ^ obj.Products.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}