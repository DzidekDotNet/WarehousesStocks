using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using WarehousesStocks.Products;
using Xunit;

namespace WarehousesStocks.Tests.Products
{
    public class ProductLineParserTest
    {
        private readonly ProductLineParser target;

        public ProductLineParserTest()
        {
            target = new ProductLineParser(new Mock<ILogger<ProductLineParser>>().Object);
        }

        [Theory]
        [MemberData(nameof(ProductsTestData))]
        public async void GetProducts_ShouldReturnValue(string input, Product expected)
        {
            Product result = await target.ParseLine(input);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.Warehouses.Count(), result.Warehouses.Count());
            foreach (ProductWarehouse productWarehouse in expected.Warehouses)
            {
                Assert.True(result.Warehouses.Any(x =>
                    x.Amount == productWarehouse.Amount && x.Name == productWarehouse.Name));
            }
        }

        public static IEnumerable<object[]> ProductsTestData =>
            new List<object[]>()
            {
                new object[]
                {
                    "MDF, CARB2, 1 1/8\";COM-101734;WH-C,8",
                    new Product("COM-101734", "MDF, CARB2, 1 1/8\"",
                        new List<ProductWarehouse>()
                        {
                            new ProductWarehouse("WH-C", 8),
                        })
                },
                new object[]
                {
                    @"Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back;3M-Cherry-10mm;WH-A,10|WH-B,1",
                    new Product("3M-Cherry-10mm",
                        "Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back",
                        new List<ProductWarehouse>()
                        {
                            new ProductWarehouse("WH-A", 10),
                            new ProductWarehouse("WH-B", 1),
                        })
                },
                new object[]
                {
                    @"Generic Wire Pull;COM-123906c;WH-A,10|WH-B,6|WH-C,2",
                    new Product("COM-123906c", "Generic Wire Pull",
                        new List<ProductWarehouse>()
                        {
                            new ProductWarehouse("WH-A", 10),
                            new ProductWarehouse("WH-B", 6),
                            new ProductWarehouse("WH-C", 2),
                        })
                },
            };
    }
}