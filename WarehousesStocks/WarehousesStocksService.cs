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
        private readonly IProductReader productReader;
        private readonly IWarehouseReportGenerator warehouseReportGenerator;
        private readonly ILogger<WarehousesStocksService> logger;

        public WarehousesStocksService(IProductReader productReader, IWarehouseReportGenerator warehouseReportGenerator,
            ILogger<WarehousesStocksService> logger)
        {
            this.productReader = productReader;
            this.warehouseReportGenerator = warehouseReportGenerator;
            this.logger = logger;
        }

        public async Task<StringBuilder> Run(Stream input)
        {
            return new StringBuilder("some text");
        }
    }
}