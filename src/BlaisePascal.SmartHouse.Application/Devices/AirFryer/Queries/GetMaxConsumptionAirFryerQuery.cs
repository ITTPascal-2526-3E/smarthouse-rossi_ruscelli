using BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Queries
{
    public class GetMaxConsumptionAirFryerQuery
    {
        private readonly IAirFryerRepository airFryerRepository;

        public GetMaxConsumptionAirFryerQuery(IAirFryerRepository airFryerRepository)
        {
            this.airFryerRepository = airFryerRepository;
        }

        public float Execute()
        {
            var airFryers = airFryerRepository.GetAll();
            if (!airFryers.Any())
            {
                throw new Exception("No airfryers found");
            }
            return airFryers.Max(a => a.GetMaxConsumption());
        }
    }
}
