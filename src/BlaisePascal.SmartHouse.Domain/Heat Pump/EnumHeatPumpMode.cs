using BlaisePascal.SmartHouse.Domain.AirFryer;
using System;

namespace BlaisePascal.SmartHouse.Domain.Heat_Pump
{
    internal class EnumHeatPumpMode
    {
        public enum HeatPumpMode
        {
            Heating,
            Cooling,
            HotWater,
            Auto,
            Eco,
            Comfort
        }
    }
}
