using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice; // Assicurati che qui sia importato il namespace corretto
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.AirFryerInterfaces;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Queries
{
    internal class GetAllAirfryerQuery
    {
        private readonly IAirFrayerRepository airfryerRepository;

        public GetAllAirfryerQuery(IAirFrayerRepository airfryerRepository)
        {
            this.airfryerRepository = airfryerRepository;
        }

        public Domain.AirFryerDevice.AirFryer[] Execute()
        {
            return airfryerRepository.GetAll().ToArray();
        }
    }
}
