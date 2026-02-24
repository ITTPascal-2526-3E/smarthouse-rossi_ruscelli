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
    internal class RemoveAirfryerCommand
    {
        private readonly IAirFrayerRepository   AirFryerRepository;
        public RemoveAirfryerCommand(IAirFrayerRepository airfryerRepository)
        {
            this.AirFryerRepository = airfryerRepository;
        }
    }
}
