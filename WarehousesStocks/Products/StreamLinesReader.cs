using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WarehousesStocks.Products
{
    internal class StreamLinesReader: IStreamLinesReader
    {
        public Task<IEnumerable<string>> ReadLines(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}