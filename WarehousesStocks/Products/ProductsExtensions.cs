using Microsoft.Extensions.DependencyInjection;

namespace WarehousesStocks.Products
{
    internal static class ProductExtensions
    {
        internal static IServiceCollection AddProducts(this IServiceCollection services)
        {
            services.AddTransient<IStreamLinesReader, StreamLinesReader>();
            return services;
        }
    }
}