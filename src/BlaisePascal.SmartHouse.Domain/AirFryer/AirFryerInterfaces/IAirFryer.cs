using BlaisePascal.SmartHouse.Domain.AirFryerDevice;
using BlaisePascal.SmartHouse.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.AirFryerDevice.AirFryerInterfaces
{
    public interface IAirFryer : IChangeableCost, ITimerSettable, IGetEnergyConsumption, IGetCost, Iswitch, ITemperatureAdjustable, IGetConsumption
    {
        float GetMaxConsumption();
        float GetMinConsumption();
    }
}
