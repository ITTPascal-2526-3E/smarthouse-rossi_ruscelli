using BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Queries
{
    public class GetAllAirfryerQuery
    {
        //ok
        private readonly IAirFryerRepository airFryerRepository;

        public GetAllAirfryerQuery(IAirFryerRepository airFryerRepository)
        {
            //ok
            this.airFryerRepository = airFryerRepository;
        }

        public Domain.AirFryerDevice.AirFryer[] Execute()
        {
            //ok
            return airFryerRepository.GetAll().ToArray();
        }
    }
}
