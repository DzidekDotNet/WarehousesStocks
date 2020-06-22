using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using WarehousesStocks.Products;
using WarehousesStocks.Warehouses;
using Xunit;

namespace WarehousesStocks.Tests.Warehouses
{
    public class WarehousesReportGeneratorTest
    {
        private readonly WarehousesReportGenerator target;

        private readonly Mock<IWarehousesReportDataCreator> warehousesReportDataCreatorMock =
            new Mock<IWarehousesReportDataCreator>();

        private readonly Mock<IWarehousesReportFormatter> warehousesReportFormatterMock =
            new Mock<IWarehousesReportFormatter>();


        public WarehousesReportGeneratorTest()
        {
            target = new WarehousesReportGenerator(
                warehousesReportDataCreatorMock.Object,
                warehousesReportFormatterMock.Object,
                new Mock<ILogger<WarehousesReportGenerator>>().Object);
        }

        [Fact]
        public async void Run_ShouldCreateDataAndFormatThem()
        {
            await target.Generate(new List<Product>());

            warehousesReportDataCreatorMock.Verify(x => x.CreateReportData(It.IsAny<IEnumerable<Product>>()),
                Times.Once);
            warehousesReportFormatterMock.Verify(x => x.GenerateWarehousesReport(It.IsAny<IEnumerable<Warehouse>>()),
                Times.Once);
        }
    }
}