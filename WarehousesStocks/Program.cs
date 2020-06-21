using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WarehousesStocks
{
    // ReSharper disable once ClassNeverInstantiated.Global
    class Program
    {
        static async Task Main(string[] args)
        {
            //In production application we should add configuration to read settings from Development|Production file.
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("loggerSettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                var serviceCollection = new ServiceCollection();
                Services.ConfigureServices(serviceCollection);

                var serviceProvider = serviceCollection.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<Program>>();
                IWarehousesStocksService service = serviceProvider.GetService<IWarehousesStocksService>();

                Stream input = Console.OpenStandardInput();
                StringBuilder output = await service.Run(input);
                await Console.Out.WriteAsync(output);
                logger.LogDebug("Output data has been sent to console");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}