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
    public class UpdateHeatPumpCommnad
    {
        private readonly IHeatPumpRepository heatPumpRepository;

        public UpdateHeatPumpCommnad(IHeatPumpRepository heatPumpRepository)
        {
            this.heatPumpRepository = heatPumpRepository;
        }

        public void Execute(Guid id, bool isOn, TemperatureDevice defaultTemperature, HeatPumpMode mode, CostPerKWh costPerKWh)
        {
            var heatPump = heatPumpRepository.GetById(id);
            if (heatPump == null)
            {
                throw new Exception("Heat Pump not found");
            }
            heatPump.Update(isOn, defaultTemperature, mode, costPerKWh);
            heatPumpRepository.Update(heatPump);
        }
    }
}
