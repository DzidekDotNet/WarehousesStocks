using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WarehousesStocks.Products
{
    internal interface IProductReader
    {
        Task<IEnumerable<Product>> GetProductsData(Stream stream);
    }
}