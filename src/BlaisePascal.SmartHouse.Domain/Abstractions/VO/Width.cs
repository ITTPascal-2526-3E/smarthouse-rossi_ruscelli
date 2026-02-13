using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstractions.VO
{
    public sealed class Width
    {
        public float Value { get; }
        public Width(float value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Width cannot be negative.", nameof(value));
            }
            Value = value;
        }
    }
}
