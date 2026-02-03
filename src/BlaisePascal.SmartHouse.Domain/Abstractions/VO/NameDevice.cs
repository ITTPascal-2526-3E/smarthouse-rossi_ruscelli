using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstractions.VO
{
    public sealed class NameDevice
    {
        public string Value { get; }

        public NameDevice(string value) { 
            if(value == null)
                throw new ArgumentNullException(nameof(value), "NameDevice cannot be null");
            else Value = value;

        }
    }
}
