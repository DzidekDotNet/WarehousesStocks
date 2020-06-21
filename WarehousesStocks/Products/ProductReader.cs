using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WarehousesStocks.Products
{
    internal class ProductReader: IProductReader
    {
        public Task<IEnumerable<Product>> GetProductsData(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}