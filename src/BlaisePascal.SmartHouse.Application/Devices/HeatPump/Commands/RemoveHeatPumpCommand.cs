using BlaisePascal.SmartHouse.Domain.Heat_Pump.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.HeatPump.Commands
{
    public class RemoveHeatPumpCommand
    {
        private readonly IHeatPumpRepository heatPumpRepository;

        public RemoveHeatPumpCommand(IHeatPumpRepository heatPumpRepository)
        {
            this.heatPumpRepository = heatPumpRepository;
        }

        public void Execute(Guid id)
        {
            var heatPump = heatPumpRepository.GetById(id);
            if (heatPump == null)
            {
                throw new Exception("Heat pump not found");
            }
            heatPumpRepository.Remove(heatPump);
        }
    }
}
