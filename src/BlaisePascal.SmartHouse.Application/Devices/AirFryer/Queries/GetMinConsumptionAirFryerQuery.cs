using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Queries
{
    public class GetMinConsumptionAirFryerQuery
    {
        //ok
        private readonly Domain.AirFryerDevice.Repositories.IAirFryerRepository airFryerRepository;
        public GetMinConsumptionAirFryerQuery(Domain.AirFryerDevice.Repositories.IAirFryerRepository airFryerRepository)
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
            return airFryers.Min(a => a.GetMinConsumption());
        }
    }
}
