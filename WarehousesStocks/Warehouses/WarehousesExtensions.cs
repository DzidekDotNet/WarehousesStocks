using Microsoft.Extensions.DependencyInjection;

namespace WarehousesStocks.Warehouses
{
    internal static class WarehousesExtensions
    {
        internal static IServiceCollection AddWarehouses(this IServiceCollection services)
        {
            services.AddTransient<IWarehouseReportGenerator, WarehouseReportGenerator>();
            return services;
        }
    }
}