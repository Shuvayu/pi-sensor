using System;

namespace PiSensorNode.Configurations
{
    public class CpuTemperatureMonitorConfiguration
    {
        public TimeSpan ReadInterval { get; set; } = TimeSpan.FromMinutes(1);
    }
}
