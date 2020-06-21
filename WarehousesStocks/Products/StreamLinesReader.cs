using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WarehousesStocks.Products
{
    internal class StreamLinesReader
    {
        internal IEnumerable<string> ReadLines(Stream input)
        {
            IEnumerable<string> lines = Enumerable.Empty<string>();
            using var stream = input;
            using var reader = new StreamReader(stream, Encoding.UTF8);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines = lines.Append(line);
            }

            return lines;
        }
    }
}