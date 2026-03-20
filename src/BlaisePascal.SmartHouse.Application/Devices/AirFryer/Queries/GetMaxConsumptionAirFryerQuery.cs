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
        //ok
        private readonly IAirFryerRepository airFryerRepository;

        public GetMaxConsumptionAirFryerQuery(IAirFryerRepository airFryerRepository)
        {
            //ok
            this.airFryerRepository = airFryerRepository;
        }

        public float Execute()
        {
            //ok
            var airFryers = airFryerRepository.GetAll();
            if (!airFryers.Any())
            {
                throw new Exception("No airfryers found");
            }
            return airFryers.Max(a => a.GetMaxConsumption());
        }
    }
}
