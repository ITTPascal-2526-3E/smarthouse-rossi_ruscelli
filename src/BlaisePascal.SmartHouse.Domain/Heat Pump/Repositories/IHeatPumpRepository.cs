using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Heat_Pump.Repositories
{
    public interface IHeatPumpRepository
    {
        void Add(HeatPump heatPump);
        void Update(HeatPump heatPump);
        void Remove(HeatPump heatPump);
        HeatPump GetById(Guid id);
        List<HeatPump> GetAll();
    }
}
