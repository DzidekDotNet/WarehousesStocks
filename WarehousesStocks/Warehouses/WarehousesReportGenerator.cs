using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WarehousesStocks.Products;

namespace WarehousesStocks.Warehouses
{
    internal class WarehousesReportGenerator : IWarehousesReportGenerator
    {
        private readonly IWarehousesReportDataCreator warehousesReportDataCreator;
        private readonly IWarehousesReportFormatter warehousesReportFormatter;
        private readonly ILogger<WarehousesReportGenerator> logger;

        public WarehousesReportGenerator(IWarehousesReportDataCreator warehousesReportDataCreator,
            IWarehousesReportFormatter warehousesReportFormatter,
            ILogger<WarehousesReportGenerator> logger)
        {
            this.warehousesReportDataCreator = warehousesReportDataCreator;
            this.warehousesReportFormatter = warehousesReportFormatter;
            this.logger = logger;
        }

        public async Task<StringBuilder> Generate(IEnumerable<Product> products)
        {
            logger.LogTrace("Generating warehouses report from products. products: '{@products}'", products);

            IEnumerable<Warehouse> warehouses = await warehousesReportDataCreator.CreateReportData(products);
            logger.LogTrace("Generating warehouses report for warehouses. warehouses: '{@warehouses}'", products,
                warehouses);

            StringBuilder report = warehousesReportFormatter.GenerateWarehousesReport(warehouses);

            logger.LogDebug("The warehouses report has been generated. text report: '{@report}'", report);
            return report;
        }
    }
}