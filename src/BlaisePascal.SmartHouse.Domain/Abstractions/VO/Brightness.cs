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
            // Ensure value is clamped between 0 and 100
            Value = Math.Clamp(value, 0, 100);
        }
       
    }
}
