using BlaisePascal.SmartHouse.Domain.AirFryer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Interfaces
{
    public interface IGetEnergyConsumption
    {
        double ConsumptionWattHours();
        double ConsumptionKiloWattHours();
    }
}
