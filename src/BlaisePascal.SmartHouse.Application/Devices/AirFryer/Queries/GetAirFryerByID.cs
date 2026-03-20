
using BlaisePascal.SmartHouse.Domain.AirFryerDevice;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Queries
{
    public class GetAirFryerByIDQuery
    {
        //ok
        private readonly IAirFryerRepository airFryerRepository;
        public GetAirFryerByIDQuery(IAirFryerRepository airFryerRepository)
        {
            //ok
            this.airFryerRepository = airFryerRepository;
        }

        public Domain.AirFryerDevice.AirFryer Execute(Guid id)
        {
            //ok
            var airFryer = airFryerRepository.GetById(id);
            if (airFryer == null)
            {
                throw new Exception("Airfryer not found");
            }
            return airFryer;
        }
    }
}
