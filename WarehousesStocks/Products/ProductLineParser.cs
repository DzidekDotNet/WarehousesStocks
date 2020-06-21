using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WarehousesStocks.Products
{
    internal class ProductLineParser : IProductLineParser
    {
        private readonly ILogger<ProductLineParser> logger;

        public ProductLineParser(ILogger<ProductLineParser> logger)
        {
            this.logger = logger;
        }

        public Task<Product> ParseLine(string line)
        {
            string[] lineData = line.Split(";");
            string productName = lineData[0];
            string productId = lineData[1];
            string warehousesData = lineData[2];
            logger.LogTrace(
                "The product data has been parsed from line. line: '{line}', productName: '{productName}', productId: '{productId}', warehousesData: '{warehousesData}'",
                line, productName, productId, warehousesData);

            IEnumerable<ProductWarehouse> warehouses = Enumerable.Empty<ProductWarehouse>();
            foreach (string warehouseData in warehousesData.Split("|"))
            {
                string[] warehouseInformation = warehouseData.Split(",");
                string warehouseId = warehouseInformation[0];
                int amount = int.Parse(warehouseInformation[1]);
                logger.LogTrace(
                    "The warehouse for product has been parsed. warehouseId: '{warehouseId}', amount: '{amount}'",
                    warehouseId, amount);

                warehouses = warehouses.Append(new ProductWarehouse(warehouseId, amount));
            }

            Product product = new Product(productId, productName, warehouses);
            logger.LogDebug("The product has been parsed from line. line: '{line}', product: '{@product}'", line,
                product);

            return Task.FromResult(product);
        }
    }
}