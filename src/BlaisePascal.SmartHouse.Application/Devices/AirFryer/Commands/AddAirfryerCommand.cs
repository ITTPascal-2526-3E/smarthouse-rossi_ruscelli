using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
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
    internal class AddAirFryerCommand // Correggi il nome della classe da "AdAirfryerCommand" a "AddAirfryerCommand"
    {
        private readonly IAirFryerRepository airFryerRepository;

        public AddAirFryerCommand(IAirFryerRepository airFryerRepository)
        {
            this.airFryerRepository = airFryerRepository;
        }
    }
}
