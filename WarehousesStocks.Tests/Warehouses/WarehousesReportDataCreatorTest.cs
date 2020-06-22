using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using WarehousesStocks.Products;
using WarehousesStocks.Warehouses;
using Xunit;

namespace WarehousesStocks.Tests.Warehouses
{
    public class WarehousesReportDataCreatorTest
    {
        private readonly WarehousesReportDataCreator target;

        public WarehousesReportDataCreatorTest()
        {
            target = new WarehousesReportDataCreator(
                new Mock<ILogger<WarehousesReportDataCreator>>().Object);
        }

        [Theory]
        [MemberData(nameof(ProductsTestData))]
        public async void CreateReportData_ShouldReturnWarehousesValue(IEnumerable<Product> input,
            IEnumerable<Warehouse> expected)
        {
            IEnumerable<Warehouse> result = await target.CreateReportData(input);
            Assert.Equal(result, expected, new WarehouseEqualityComparer());
        }

        public static IEnumerable<object[]> ProductsTestData =>
            new List<object[]>()
            {
                new object[]
                {
                    new List<Product>()
                    {
                        new Product("COM-101734", "MDF, CARB2, 1 1/8\"",
                            new List<ProductWarehouse>()
                            {
                                new ProductWarehouse("WH-C", 8),
                            })
                    },
                    new List<Warehouse>()
                    {
                        new Warehouse("WH-C", new List<WarehouseProduct>()
                        {
                            new WarehouseProduct("COM-101734", 8)
                        })
                    }
                },
                new object[]
                {
                    new List<Product>()
                    {
                        new Product("3M-Cherry-10mm",
                            "Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back",
                            new List<ProductWarehouse>()
                            {
                                new ProductWarehouse("WH-A", 10),
                            })
                    },
                    new List<Warehouse>()
                    {
                        new Warehouse("WH-A", new List<WarehouseProduct>()
                        {
                            new WarehouseProduct("3M-Cherry-10mm", 10)
                        })
                    }
                },
                new object[]
                {
                    new List<Product>()
                    {
                        new Product("3M-Cherry-10mm",
                            "Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back",
                            new List<ProductWarehouse>()
                            {
                                new ProductWarehouse("WH-A", 10),
                                new ProductWarehouse("WH-B", 10),
                                new ProductWarehouse("WH-C", 15),
                            })
                    },
                    new List<Warehouse>()
                    {
                        new Warehouse("WH-C", new List<WarehouseProduct>()
                        {
                            new WarehouseProduct("3M-Cherry-10mm", 15)
                        }),
                        new Warehouse("WH-B", new List<WarehouseProduct>()
                        {
                            new WarehouseProduct("3M-Cherry-10mm", 10)
                        }),
                        new Warehouse("WH-A", new List<WarehouseProduct>()
                        {
                            new WarehouseProduct("3M-Cherry-10mm", 10)
                        })
                    },
                },
                new object[]
                {
                    new List<Product>()
                    {
                        new Product("A",
                            "A",
                            new List<ProductWarehouse>()
                            {
                                new ProductWarehouse("WH-A", 10),
                            }),
                        new Product("C",
                            "C",
                            new List<ProductWarehouse>()
                            {
                                new ProductWarehouse("WH-A", 11),
                            }),
                        new Product("B",
                            "B",
                            new List<ProductWarehouse>()
                            {
                                new ProductWarehouse("WH-A", 12),
                            })
                    },
                    new List<Warehouse>()
                    {
                        new Warehouse("WH-A", new List<WarehouseProduct>()
                        {
                            new WarehouseProduct("A", 10),
                            new WarehouseProduct("B", 12),
                            new WarehouseProduct("C", 11)
                        })
                    }
                },
            };
    }
}