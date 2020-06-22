using System.Collections.Generic;
using System.Text;
using WarehousesStocks.Warehouses;
using Xunit;

namespace WarehousesStocks.Tests.Warehouses
{
    public class WarehousesReportFormatterTest
    {
        private readonly WarehousesReportFormatter target;

        public WarehousesReportFormatterTest()
        {
            target = new WarehousesReportFormatter();
        }
        
        [Theory]
        [MemberData(nameof(WarehousesTestData))]
        public void GenerateWarehousesReport(IEnumerable<Warehouse> input,string expected)
        {
            StringBuilder result = target.GenerateWarehousesReport(input);
            
            Assert.Equal(expected, result.ToString());
        }
        
        public static IEnumerable<object[]> WarehousesTestData =>
            new List<object[]>()
            {
                new object[]
                {
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
                    @"WH-C (total 15)
3M-Cherry-10mm: 15

WH-B (total 10)
3M-Cherry-10mm: 10

WH-A (total 10)
3M-Cherry-10mm: 10"
                },
                new object[]
                {
                    new List<Warehouse>()
                    {
                        new Warehouse("WH-A", new List<WarehouseProduct>()
                        {
                            new WarehouseProduct("A", 10),
                            new WarehouseProduct("B", 12),
                            new WarehouseProduct("C", 11)
                        })
                    },
                    @"WH-A (total 33)
A: 10
B: 12
C: 11"
                },
            };
    }
}