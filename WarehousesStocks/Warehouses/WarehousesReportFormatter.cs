using System;
using System.Collections.Generic;
using System.Text;

namespace WarehousesStocks.Warehouses
{
    public class WarehousesReportFormatter: IWarehousesReportFormatter
    {
        public StringBuilder GenerateWarehousesReport(IEnumerable<Warehouse> warehouses)
        {
            StringBuilder report = new StringBuilder();
            foreach (Warehouse warehouse in warehouses)
            {
                if (report.Length > 0)
                {
                    report.Append($"{Environment.NewLine}{Environment.NewLine}");
                }

                report.Append(GenerateWarehouseReport(warehouse));
            }

            return report;
        }

        private StringBuilder GenerateWarehouseReport(Warehouse warehouse)
        {
            StringBuilder data = new StringBuilder();
            data.Append($"{warehouse.Name} (total {warehouse.Total})");
            foreach (WarehouseProduct warehouseProduct in warehouse.Products)
            {
                data.Append($"{Environment.NewLine}{warehouseProduct.ProductId}: {warehouseProduct.Amount}");
            }

            return data;
        }
    }
}