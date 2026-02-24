using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Queries
{
    public class ConsumptionKiloWattHoursAirFryerQuery
    {
        private readonly Domain.AirFryerDevice.Repositories.IAirFryerRepository airFryerRepository;
        public ConsumptionKiloWattHoursAirFryerQuery(Domain.AirFryerDevice.Repositories.IAirFryerRepository airFryerRepository)
        {
            this.airFryerRepository = airFryerRepository;
        }
        public double Execute(Guid id)
        {
            var airFryer = airFryerRepository.GetById(id);
            if (airFryer == null)
            {
                throw new Exception("Air fryer not found");
            }
            return airFryer.ConsumptionKiloWattHours();
        }
    }
}