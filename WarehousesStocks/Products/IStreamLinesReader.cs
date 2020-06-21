using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WarehousesStocks.Products
{
    internal interface IStreamLinesReader
    {
        Task<IEnumerable<string>> ReadLines(Stream stream);
    }
}