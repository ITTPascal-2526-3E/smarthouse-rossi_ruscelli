using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstractions.VO
{
    public sealed class TemperatureDevice
    {
        public int Value { get; }
        public TemperatureDevice(int value) { 
            if (value < -50 || value > 250)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Temperature must be between -50 and 250 degrees Celsius.");
            }
            Value = value;
        }
    }
}
