using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WarehousesStocks.Products;

namespace WarehousesStocks.Warehouses
{
    internal class WarehouseReportGenerator: IWarehouseReportGenerator
    {
        public Task<StringBuilder> Generate(IEnumerable<Product> products)
        {
            throw new System.NotImplementedException();
        }
    }
}