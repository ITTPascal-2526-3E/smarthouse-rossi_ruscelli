using BlaisePascal.SmartHouse.Domain.Heat_Pump;
using BlaisePascal.SmartHouse.Domain.Heat_Pump.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.HeatPump.Commands
{
    public class ChangeModeHeatPumpCommand
    {
        private readonly IHeatPumpRepository heatPumpRepository;

        public ChangeModeHeatPumpCommand(IHeatPumpRepository heatPumpRepository)
        {
            this.heatPumpRepository = heatPumpRepository;
        }

        public void Execute(Guid id, HeatPumpMode newMode)
        {
            var heatPump = heatPumpRepository.GetById(id);
            if (heatPump == null)
            {
                throw new Exception("Heat Pump not found");
            }
            heatPump.ChangeMode(newMode);
            heatPumpRepository.Update(heatPump);
        }
    }
}
