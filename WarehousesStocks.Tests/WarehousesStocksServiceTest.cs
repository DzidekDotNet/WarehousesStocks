using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Moq;
using WarehousesStocks.Products;
using WarehousesStocks.Warehouses;
using Xunit;

namespace WarehousesStocks.Tests
{
    public class WarehousesStocksServiceTest
    {
        private readonly WarehousesStocksService target;
        private readonly Mock<IProductsReader> productReaderMock = new Mock<IProductsReader>();

        private readonly Mock<IWarehousesReportGenerator> warehouseReportGeneratorMock =
            new Mock<IWarehousesReportGenerator>();


        public WarehousesStocksServiceTest()
        {
            target = new WarehousesStocksService(
                productReaderMock.Object,
                warehouseReportGeneratorMock.Object,
                new Mock<ILogger<WarehousesStocksService>>().Object);
        }

        [Fact]
        public async void Run_ShouldReadProductsAndGenerateReport()
        {
            await target.Run(GenerateInputStreamHelper.GenerateInputStream(string.Empty));

            productReaderMock.Verify(x => x.GetProducts(It.IsAny<Stream>()), Times.Once);
            warehouseReportGeneratorMock.Verify(x => x.Generate(It.IsAny<IEnumerable<Product>>()), Times.Once);
        }
    }
}