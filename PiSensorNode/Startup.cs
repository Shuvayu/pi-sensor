using Microsoft.Extensions.Logging;
using PiSensorNode.Implementations;
using PiSensorNode.Loggers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PiSensorNode
{
    internal class Startup
    {
        private readonly ILogger<Startup> _logger;
        private readonly ICpuTemperatureMonitor _cpuTemperatureMonitor;
        private readonly IConfigurationLogger _configurationLogger;

        public Startup(ILogger<Startup> logger, ICpuTemperatureMonitor cpuTemperatureMonitor, IConfigurationLogger configurationLogger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cpuTemperatureMonitor = cpuTemperatureMonitor;
            _configurationLogger = configurationLogger;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Raspberry Pi Sensor Node started.");
            _configurationLogger.LogConfiguration();

            await _cpuTemperatureMonitor.RunAsync(cancellationToken);
        }
    }
}
