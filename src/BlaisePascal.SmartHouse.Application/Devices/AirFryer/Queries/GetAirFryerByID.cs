using BlaisePascal.SmartHouse.Domain.AirFryerDevice; // Assicurati che qui sia importato il namespace corretto
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.AirFryerInterfaces;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories;
using BlaisePascal.SmartHouse.Domain.Door.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Queries
{
    internal class GetAirFryerByIDQuery
    {
        private readonly IAirFrayerRepository airfryerRepository;

        public GetAirFryerByIDQuery(IAirFrayerRepository airfryerRepository)
        {
            this.airfryerRepository = airfryerRepository;
        }

        public Domain.AirFryerDevice.AirFryer Execute(Guid id)
        {
            return airfryerRepository.GetById(id);
        }
    }
}
