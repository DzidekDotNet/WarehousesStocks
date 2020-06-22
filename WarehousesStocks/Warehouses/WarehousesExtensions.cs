using Microsoft.Extensions.DependencyInjection;

namespace WarehousesStocks.Warehouses
{
    internal static class WarehousesExtensions
    {
        internal static IServiceCollection AddWarehouses(this IServiceCollection services)
        {
            services
                .AddTransient<IWarehousesReportGenerator, WarehousesReportGenerator>()
                .AddTransient<IWarehousesReportDataCreator, WarehousesReportDataCreator>()
                .AddTransient<IWarehousesReportFormatter, WarehousesReportFormatter>();
            return services;
        }
    }
}