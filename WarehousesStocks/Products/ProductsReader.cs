using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WarehousesStocks.Products
{
    internal class ProductsReader : IProductsReader
    {
        private readonly StreamLinesReader streamLinesReader;
        private readonly IProductLineParser productLineParser;
        private readonly ILogger<ProductsReader> logger;

        public ProductsReader(StreamLinesReader streamLinesReader, IProductLineParser productLineParser,
            ILogger<ProductsReader> logger)
        {
            this.streamLinesReader = streamLinesReader;
            this.productLineParser = productLineParser;
            this.logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProducts(Stream stream)
        {
            List<Product> products = new List<Product>();

            IEnumerable<string> lines = streamLinesReader.ReadLines(stream);
            logger.LogTrace("The following lines have been read from input. lines: '{@lines}'", lines);

            foreach (string line in lines)
            {
                if (IsLineValid(line))
                {
                    Product product = await productLineParser.ParseLine(line);
                    products.Add(product);
                }
                else
                {
                    logger.LogWarning("The following line has been invalid. line: '{line}'", line);
                }
            }

            logger.LogDebug("The following products have been read from input. products: '{@products}'", products);
            return products;
        }

        private bool IsLineValid(string line)
        {
            return !line.StartsWith("#");
        }
    }
}