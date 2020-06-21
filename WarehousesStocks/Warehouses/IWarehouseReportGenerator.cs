using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WarehousesStocks.Products;

namespace WarehousesStocks.Warehouses
{
    internal interface IWarehouseReportGenerator
    {
        Task<StringBuilder> Generate(IEnumerable<Product> products);
    }
}