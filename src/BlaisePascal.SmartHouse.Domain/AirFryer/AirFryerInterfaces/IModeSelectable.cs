using BlaisePascal.SmartHouse.Domain.AirFryerDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.AirFryerDevice.AirFryerInterfaces
{
    public interface IModeSelectable
    {
        void SelectMode(Mode mode);
    }
}
