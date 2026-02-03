using BlaisePascal.SmartHouse.Domain.Heat_Pump;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Interfaces
{
    public interface IHeatPump : IGetDoubleConsumption, IGetDoubleTemperature, Iswitch, ITemperatureAdjustable
    {
        void ChangeMode(HeatPumpMode newMode);
    }
}
