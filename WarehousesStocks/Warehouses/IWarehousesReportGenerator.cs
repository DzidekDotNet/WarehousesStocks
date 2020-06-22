using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WarehousesStocks.Products;

namespace WarehousesStocks.Warehouses
{
    internal interface IWarehousesReportGenerator
    {
        Task<StringBuilder> Generate(IEnumerable<Product> products);
    }
}