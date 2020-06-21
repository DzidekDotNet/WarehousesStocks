using System.IO;

namespace WarehousesStocks.Tests
{
    internal static class GenerateInputStreamHelper
    {
        internal static Stream GenerateInputStream(string input)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(input);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}