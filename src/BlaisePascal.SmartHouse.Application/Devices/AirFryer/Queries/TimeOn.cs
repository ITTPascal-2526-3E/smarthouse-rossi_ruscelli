using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.AirFryer.Queries
{
    public class TimeOn
    {
        private readonly Domain.AirFryerDevice.Repositories.IAirFryerRepository airFryerRepository;

        public TimeOn(Domain.AirFryerDevice.Repositories.IAirFryerRepository airFryerRepository)
        {
            this.airFryerRepository = airFryerRepository;
        }

        public TimeSpan Execute(Guid id)
        {
            var airFryer = airFryerRepository.GetById(id);
            if (airFryer == null)
            {
                throw new Exception("Air fryer not found");
            }
            return airFryer.TimeOn();
        }
    }
}
