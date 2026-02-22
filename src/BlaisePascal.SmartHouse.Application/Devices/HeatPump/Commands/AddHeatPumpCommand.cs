using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using BlaisePascal.SmartHouse.Domain.Heat_Pump;
using BlaisePascal.SmartHouse.Domain.Heat_Pump.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.HeatPump.Commands
{
    public class AddHeatPumpCommand
    {
        private readonly IHeatPumpRepository heatPumpRepository;

        public AddHeatPumpCommand(IHeatPumpRepository heatPumpRepository)
        {
            this.heatPumpRepository = heatPumpRepository;
        }

        public Domain.Heat_Pump.HeatPump Execute(NameDevice name, TemperatureDevice defaultTemperature, HeatPumpMode mode, CostPerKWh costPerKWh)
        {
            var heatPump = new Domain.Heat_Pump.HeatPump(false, defaultTemperature, mode, costPerKWh, name);
            heatPumpRepository.Add(heatPump);
            return heatPump;
        }
    }
}
