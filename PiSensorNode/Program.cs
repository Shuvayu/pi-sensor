using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PiSensorNode.Configurations;
using PiSensorNode.Implementations;
using PiSensorNode.Loggers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PiSensorNode
{
    internal class Program
    {
        private readonly static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private static async Task Main(string[] args)
        {
            var serviceProvider = BuildCompositionRoot(new ServiceCollection());

            // Entry Point
            Console.CancelKeyPress += HandleExit;
            await serviceProvider.GetService<Startup>().RunAsync(_cancellationTokenSource.Token);


            (serviceProvider as IDisposable)?.Dispose();
        }

        private static ServiceProvider BuildCompositionRoot(IServiceCollection servicesCollection)
        {
            // Configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .AddLogging(loggingBuilder => loggingBuilder.AddConsole())
                .Configure<AzureIoTHubConfiguration>(configuration.GetSection("AzureIoTHub"))
                .Configure<CpuTemperatureMonitorConfiguration>(configuration.GetSection("CpuTemperatureMonitor"))
                .AddTransient<IConfigurationLogger, ConfigurationLogger>()
                .AddTransient<ITemperatureReader, RaspberryPiCpuTemperatureReader>()
                .AddTransient<ICpuTemperatureMonitor, CpuTemperatureMonitor>()
                .AddTransient<Startup>()
                .BuildServiceProvider();

            return serviceProvider;
        }

        private static void HandleExit(object sender, ConsoleCancelEventArgs eventArguments)
        {
            eventArguments.Cancel = true;
            _cancellationTokenSource.Cancel();
        }
    }
}
