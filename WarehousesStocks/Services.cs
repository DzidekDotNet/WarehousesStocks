using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using WarehousesStocks.Products;
using WarehousesStocks.Warehouses;

namespace WarehousesStocks
{
    internal static class Services
    {
        internal static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddLogging(configure => configure.AddSerilog())
                .AddTransient<IWarehousesStocksService, WarehousesStocksService>()
                .AddProducts()
                .AddWarehouses();
        }
    }
}