using System.Collections.Generic;
using WarehousesStocks.Warehouses;

namespace WarehousesStocks.Tests.Warehouses
{
    public class WarehouseProductEqualityComparer : IEqualityComparer<WarehouseProduct>
    {
        public bool Equals(WarehouseProduct actual, WarehouseProduct expected)
        {
            if (actual == null && expected == null)
                return true;
            else if (expected == null || actual == null)
                return false;
            else if (expected.ProductId == actual.ProductId
                     && expected.Amount == actual.Amount)
                return true;
            else
                return false;
        }

        public int GetHashCode(WarehouseProduct obj)
        {
            long hCode = obj.ProductId.GetHashCode() ^ obj.Amount;
            return hCode.GetHashCode();
        }
    }
}