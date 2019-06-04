using System.Threading;
using System.Threading.Tasks;

namespace PiSensorNode.Implementations
{
    internal interface ICpuTemperatureMonitor
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
