using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WarehousesStocks.Products;
using WarehousesStocks.Warehouses;

namespace WarehousesStocks
{
    internal class WarehousesStocksService : IWarehousesStocksService
    {
        private readonly IProductsReader productsReader;
        private readonly IWarehousesReportGenerator warehousesReportGenerator;
        private readonly ILogger<WarehousesStocksService> logger;

        public WarehousesStocksService(IProductsReader productsReader,
            IWarehousesReportGenerator warehousesReportGenerator,
            ILogger<WarehousesStocksService> logger)
        {
            this.productsReader = productsReader;
            this.warehousesReportGenerator = warehousesReportGenerator;
            this.logger = logger;
        }

        public async Task<StringBuilder> Run(Stream input)
        {
            IEnumerable<Product> products = await productsReader.GetProducts(input);
            logger.LogTrace("The following products have been read from input. products: '{@products}'", products);

            StringBuilder warehousesTextReport = await warehousesReportGenerator.Generate(products);
            logger.LogDebug(
                "The following report has been generated from input. warehousesTextReport: '{@warehousesTextReport}', products: '{@products}'",
                warehousesTextReport, products);

            return warehousesTextReport;
        }
    }
}