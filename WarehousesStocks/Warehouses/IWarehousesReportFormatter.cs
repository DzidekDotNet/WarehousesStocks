using System.Collections.Generic;
using System.Text;

namespace WarehousesStocks.Warehouses
{
    public interface IWarehousesReportFormatter
    {
        StringBuilder GenerateWarehousesReport(IEnumerable<Warehouse> warehouses);
    }
}