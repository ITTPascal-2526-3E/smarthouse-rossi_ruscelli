using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstractions.VO
{
    public sealed class ConsumptionDevice
    {
        public int Consumption { get; private set; } // Consumption in watts
        public ConsumptionDevice(int consumption)
        {
            if (consumption < 0)
                throw new ArgumentException("Consumption cannot be negative.");
            Consumption = consumption;
        }
    }
}
