using PiSensorNode.Models;

namespace PiSensorNode.Implementations
{
    public interface ITemperatureReader
    {
        TemperatureReading Read();
    }
}
