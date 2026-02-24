using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Commands
{
    public class UpdateAirFryerCommans
    {
        private readonly Domain.AirFryerDevice.Repositories.IAirFryerRepository airFryerRepository;

        public UpdateAirFryerCommans(Domain.AirFryerDevice.Repositories.IAirFryerRepository airFryerRepository)
        {
            this.airFryerRepository = airFryerRepository;
        }

        public void Execute(Guid id, string name, bool isOn, int temperature)
        {
            var airFryer = airFryerRepository.GetById(id);
            if (airFryer == null)
            {
                throw new Exception("Air fryer not found");
            }
            airFryer.Update(name, isOn, temperature);
            airFryerRepository.Update(airFryer);
        }
    }
}
