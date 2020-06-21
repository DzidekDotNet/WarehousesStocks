using Microsoft.Extensions.DependencyInjection;

namespace WarehousesStocks.Products
{
    internal static class ProductExtensions
    {
        internal static IServiceCollection AddProducts(this IServiceCollection services)
        {
            services.AddTransient<StreamLinesReader>()
                .AddTransient<IProductsReader, ProductsReader>()
                .AddTransient<IProductLineParser, ProductLineParser>();
            return services;
        }
    }
}