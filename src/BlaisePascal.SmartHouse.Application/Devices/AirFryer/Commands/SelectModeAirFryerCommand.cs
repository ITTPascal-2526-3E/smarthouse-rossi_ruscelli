using BlaisePascal.SmartHouse.Domain.AirFryerDevice;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Commands
{
    public class SelectModeAirFryerCommand
    {
        private readonly IAirFryerRepository airFryerRepository;

        public SelectModeAirFryerCommand(IAirFryerRepository airFryerRepository)
        {
            this.airFryerRepository = airFryerRepository;
        }

        public void Execute(Guid id, Mode mode)
        {
            var airFryer = airFryerRepository.GetById(id);
            if (airFryer == null)
            {
                throw new Exception("Airfryer not found");
            }
            airFryer.SelectMode(mode);
        }
    }
}
