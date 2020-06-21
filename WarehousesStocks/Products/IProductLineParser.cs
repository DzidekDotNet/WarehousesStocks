using System.Threading.Tasks;

namespace WarehousesStocks.Products
{
    internal interface IProductLineParser
    {
        Task<Product> ParseLine(string line);
    }
}