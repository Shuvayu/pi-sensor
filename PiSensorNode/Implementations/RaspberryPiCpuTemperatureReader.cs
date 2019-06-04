using Iot.Device.CpuTemperature;
using PiSensorNode.Models;

namespace PiSensorNode.Implementations
{
    public class RaspberryPiCpuTemperatureReader : ITemperatureReader
    {
        private readonly CpuTemperature _cpuTemperature;

        public RaspberryPiCpuTemperatureReader()
        {
            _cpuTemperature = new CpuTemperature();
        }

        public TemperatureReading Read()
        {
            return new TemperatureReading(_cpuTemperature.IsAvailable, _cpuTemperature.Temperature.Celsius);
        }
    }
}
