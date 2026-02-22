using BlaisePascal.SmartHouse.Domain.Heat_Pump.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.HeatPump.Queries
{
    public class GetAllHeatPumpsQuery
    {
        private readonly IHeatPumpRepository heatPumpRepository;

        public GetAllHeatPumpsQuery(IHeatPumpRepository heatPumpRepository)
        {
            this.heatPumpRepository = heatPumpRepository;
        }

        public Domain.Heat_Pump.HeatPump[] Execute()
        {
            return heatPumpRepository.GetAll().ToArray();
        }
    }
}
