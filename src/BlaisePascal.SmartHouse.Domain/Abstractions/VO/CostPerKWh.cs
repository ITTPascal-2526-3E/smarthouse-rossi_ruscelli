using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstractions.VO
{
    public sealed class CostPerKWh
    {
        public float Value { get; }
        public CostPerKWh(float value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Cost per kWh cannot be negative.", nameof(value));
            }
            Value = value;
        }
    }
}
