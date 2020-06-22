using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using WarehousesStocks.Products;
using Xunit;

namespace WarehousesStocks.Tests.Products
{
    public class ProductReaderTest
    {
        private readonly ProductsReader target;
        private readonly Mock<IProductLineParser> productParserMock = new Mock<IProductLineParser>();

        private static readonly Product product1 = new Product(
            "COM-100001",
            "Cherry Hardwood Arched Door - PS",
            new List<ProductWarehouse>()
            {
                new ProductWarehouse("WH-A", 5),
                new ProductWarehouse("WH-B", 10),
            });
        
        private static readonly Product product2 = new Product(
            "COM-124047", 
            "Maple Dovetail Drawerbox",
            new List<ProductWarehouse>()
            {
                new ProductWarehouse("WH-A", 15)
            });
        

        public ProductReaderTest()
        {
            productParserMock
                .SetupSequence(x => x.ParseLine(It.IsAny<string>()))
                .ReturnsAsync(product1)
                .ReturnsAsync(product2);
                
            target = new ProductsReader(
                new StreamLinesReader(), 
                productParserMock.Object,
                new Mock<ILogger<ProductsReader>>().Object);
        }

        [Theory]
        [MemberData(nameof(ProductsTestData))]
        public async void GetProducts_ShouldReturnExpectedValue(string input, IEnumerable<Product> expected)
        {
            IEnumerable<Product> result =
                await target.GetProducts(GenerateInputStreamHelper.GenerateInputStream(input));
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> ProductsTestData =>
            new List<object[]>()
            {
                new object[] {"# some comment", new List<Product>()},
                new object[]
                {
                    @"# New materials
                    Cherry Hardwood Arched Door - PS;COM-100001;WH-A,5|WH-B,10
                    Maple Dovetail Drawerbox;COM-124047;WH-A,15",
                    new List<Product>()
                    {
                        product1,
                        product2,
                    },
                },
            };
    }
}