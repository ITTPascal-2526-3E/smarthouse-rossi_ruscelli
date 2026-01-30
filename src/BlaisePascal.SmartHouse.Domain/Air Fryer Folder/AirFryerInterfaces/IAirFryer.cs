using BlaisePascal.SmartHouse.Domain.AirFryer;
using BlaisePascal.SmartHouse.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.AirFryer.AirFryerInterfaces
{
    public interface IAirFryer : IChangeableCost, ITimerSettable, IModeSelectable, IGetEnergyConsumption, IGetCost, Iswitch, ITemperatureAdjustable, IGetConsumption
    {
        int GetMaxConsumption();
        float GetMinConspumption();
       
    }
}
