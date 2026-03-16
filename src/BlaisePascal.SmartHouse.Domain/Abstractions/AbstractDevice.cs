using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.Abstractions
{
    public abstract class AbstractDevice
    {
        protected Guid Id; // Unique identifier for the device
        protected NameDevice Name; // Name of the device
        public AbstractDevice(NameDevice name)
        {
            Name = name;
            Id = Guid.NewGuid();
            // synchronize public properties so callers can access them
            Idproperty = Id;
            Nameproperty = name.Value;
        }
        public Guid Idproperty { get; set; }
        public string Nameproperty { get; set; }
    }
}
