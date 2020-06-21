using System.Collections.Generic;
using System.IO;
using System.Text;
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

        private readonly Mock<IWarehouseReportGenerator> warehouseReportGeneratorMock =
            new Mock<IWarehouseReportGenerator>();


        public WarehousesStocksServiceTest()
        {
            target = new WarehousesStocksService(
                productReaderMock.Object,
                warehouseReportGeneratorMock.Object,
                new Mock<ILogger<WarehousesStocksService>>().Object);
        }

        [Theory]
        [InlineData(
            @"# Material inventory initial state as of Jan 01 2018
# New materials
Cherry Hardwood Arched Door - PS;COM-100001;WH-A,5|WH-B,10
Maple Dovetail Drawerbox;COM-124047;WH-A,15
Generic Wire Pull;COM-123906c;WH-A,10|WH-B,6|WH-C,2
Yankee Hardware 110 Deg. Hinge;COM-123908;WH-A,10|WH-B,11
# Existing materials, restocked
Hdw Accuride CB0115-CASSRC - Locking Handle Kit - Black;CB0115-CASSRC;WH-C,13|WH-B,5
Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back;3M-Cherry-10mm;WH-A,10|WH-B,1
Veneer - Cherry Rotary 1 FSC;COM-123823;WH-C,10
MDF, CARB2, 1 1/8"";COM-101734;WH-C,8",
            @"WH-A (total 50)
3M-Cherry-10mm: 10
COM-100001: 5
COM-123906c: 10
COM-123908: 10
COM-124047: 15

WH-C (total 33)
CB0115-CASSRC: 13
COM-101734: 8
COM-123823: 10
COM-123906c: 2

WH-B (total 33)
3M-Cherry-10mm: 1
CB0115-CASSRC: 5
COM-100001: 10
COM-123906c: 6
COM-123908: 11")]
        [InlineData(
            @"# Some comment
Product | name , with special characters:,|;Product Id 1;WH-1A,5|WH-B,10|WH-C,5
# Some comment
Product name;Product Id 2;WH-A,15|WH-C,5
# Some comment
Product name;Product Id 3;WH-B,15
# Some comment
Product name;Product Id 4;WH-A,1|WH-C,11
# Some comment
Product name;Product Id 5;WH-B,1",
            @"WH-B (total 26)
Product Id 1: 10
Product Id 3: 15
Product Id 5: 1

WH-C (total 21)
Product Id 1: 5
Product Id 2: 5
Product Id 4: 11

WH-A (total 21)
Product Id 1: 5
Product Id 2: 15
Product Id 4: 1")]
        public async void Run_ShouldReturnValue(string input, string expected)
        {
            StringBuilder result = await target.Run(GenerateInputStreamHelper.GenerateInputStream(input));

            productReaderMock.Verify(x => x.GetProducts(It.IsAny<Stream>()), Times.Once);
            warehouseReportGeneratorMock.Verify(x => x.Generate(It.IsAny<IEnumerable<Product>>()), Times.Once);

            Assert.Equal(expected, result.ToString());
        }
    }
}