using BlaisePascal.SmartHouse.Domain.AirFryerDevice.AirFryerInterfaces;
using BlaisePascal.SmartHouse.Domain.Door.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice; // Assicurati che qui sia importato il namespace corretto
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.AirFryerInterfaces;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Commands
{
    internal class RemoveAirFryerCommand
    {
        private readonly IAirFryerRepository airFryerRepository;

        public RemoveAirFryerCommand(IAirFryerRepository airFryerRepository)
        {
            this.airFryerRepository = airFryerRepository;
        }

        public void Execute(Guid id)
        {
            var airFryer = airFryerRepository.GetById(id);
            if (airFryer == null)
            {
                throw new Exception("Air Fryer not found");
            }
            airFryerRepository.Remove(airFryer);
        }
    }
}
