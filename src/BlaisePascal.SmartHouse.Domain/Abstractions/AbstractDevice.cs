using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstractions
{
    public abstract class AbstractDevice
    {
        protected Guid Id; // Unique identifier for the device
        protected string Name; // Name of the device
        public AbstractDevice(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
        public Guid Idproperty { get; private set; }
        public string Nameproperty { get; set; }
    }
}
