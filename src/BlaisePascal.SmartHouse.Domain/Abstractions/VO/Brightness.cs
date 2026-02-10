using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstractions.VO
{
    public sealed class Brightness
    {
        public int Value { get; }

        public Brightness(int value) { 
        
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Brightness must be between 0 and 100.");
            }
            Value = value;
        }
    }
}
