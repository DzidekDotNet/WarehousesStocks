using System.Collections.Generic;
using System.Threading.Tasks;
using WarehousesStocks.Products;

namespace WarehousesStocks.Warehouses
{
    public interface IWarehousesReportDataCreator
    {
        Task<IEnumerable<Warehouse>> CreateReportData(IEnumerable<Product> products);
    }
}