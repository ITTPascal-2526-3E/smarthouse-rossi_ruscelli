using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Commands
{
    public class SetTempAirFryerCommand
    {
        private readonly IAirFryerRepository airFryerRepository;
    
            public SetTempAirFryerCommand(IAirFryerRepository airFryerRepository)
            {
                this.airFryerRepository = airFryerRepository;
            }
    
            public void Execute(Guid id, TemperatureDevice temperature)
            {
                var airFryer = airFryerRepository.GetById(id);
                if (airFryer == null)
                {
                    throw new Exception("Airfryer not found");
                }
                airFryer.SetTemp(temperature);
        }
    }
}
