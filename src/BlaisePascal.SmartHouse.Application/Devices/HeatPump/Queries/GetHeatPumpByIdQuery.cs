using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.HeatPump.Queries
{
    public class GetHeatPumpByIdQuery
    {
        private readonly Domain.Heat_Pump.Repositories.IHeatPumpRepository heatPumpRepository;
        public GetHeatPumpByIdQuery(Domain.Heat_Pump.Repositories.IHeatPumpRepository heatPumpRepository)
        {
            this.heatPumpRepository = heatPumpRepository;
        }
        public Domain.Heat_Pump.HeatPump Execute(Guid id)
        {
            return heatPumpRepository.GetById(id);
        }
    }
}
