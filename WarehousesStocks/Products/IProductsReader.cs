using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WarehousesStocks.Products
{
    internal interface IProductsReader
    {
        Task<IEnumerable<Product>> GetProducts(Stream stream);
    }
}