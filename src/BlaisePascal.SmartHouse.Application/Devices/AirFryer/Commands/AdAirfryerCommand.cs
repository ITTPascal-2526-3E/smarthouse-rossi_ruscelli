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
    internal class AddAirfryerCommand // Correggi il nome della classe da "AdAirfryerCommand" a "AddAirfryerCommand"
    {
        private readonly IAirFrayerRepository AirfryerRepository;

        public AddAirfryerCommand(IAirFrayerRepository AirFryerRepository)
        {
            this.AirfryerRepository = AirFryerRepository;
        }
        // TemperatureDevice temp, TemperatureDevice maxTemp, bool isOn, CostPerKWh costPerKWh,NameDevice name, Mode Mode

        public void Execute(TemperatureDevice temp, TemperatureDevice maxTemp, bool isOn, CostPerKWh costPerKWh, NameDevice name, Mode mode)
        {
            var airfryer = new BlaisePascal.SmartHouse.Domain.AirFryerDevice.AirFryer(temp, maxTemp, isOn, costPerKWh, name, mode);
            AirfryerRepository.Add(airfryer);
        }
    }
}
