using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WarehousesStocks.Products;

namespace WarehousesStocks.Warehouses
{
    internal class WarehousesReportDataCreator : IWarehousesReportDataCreator
    {
        private readonly ILogger<WarehousesReportDataCreator> logger;

        public WarehousesReportDataCreator(ILogger<WarehousesReportDataCreator> logger)
        {
            this.logger = logger;
        }

        public Task<IEnumerable<Warehouse>> CreateReportData(IEnumerable<Product> products)
        {
            IList<Warehouse> warehouses = new List<Warehouse>();
            logger.LogTrace("Generating warehouses report data from products. products: '{@products}'", products);

            foreach (Product product in products)
            {
                foreach (ProductWarehouse productWarehouse in product.Warehouses)
                {
                    // ReSharper disable once SimplifyLinqExpression
                    if (!warehouses.Any(x => x.Name == productWarehouse.Name))
                    {
                        warehouses.Add(new Warehouse(productWarehouse.Name, new List<WarehouseProduct>()));
                    }

                    warehouses.First(x => x.Name == productWarehouse.Name)
                        .AddProduct(new WarehouseProduct(product.Id, productWarehouse.Amount));
                }
            }

            IEnumerable<Warehouse> sortedWarehouses = SortData(warehouses);

            logger.LogDebug(
                "The warehouses data for report has been generated. products: '{@products}', warehouses: '{@warehouses}'",
                products, sortedWarehouses);

            return Task.FromResult(sortedWarehouses.AsEnumerable());
        }

        private IEnumerable<Warehouse> SortData(IList<Warehouse> warehouses)
        {
            IList<Warehouse> sortedWarehouses =
                warehouses.OrderByDescending(x => x.Total).ThenByDescending(x => x.Name).ToList();

            foreach (Warehouse warehouse in sortedWarehouses)
            {
                warehouse.SetProducts(warehouse.Products.OrderBy(x => x.ProductId));
            }

            return sortedWarehouses;
        }
    }
}