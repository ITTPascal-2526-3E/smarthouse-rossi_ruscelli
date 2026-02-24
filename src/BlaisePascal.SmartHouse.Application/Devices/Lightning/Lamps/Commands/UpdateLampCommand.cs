using BlaisePascal.SmartHouse.Domain.Lamps.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Lightning.Lamps.Commands
{
    public class UpdateLampCommand
    {
        private readonly ILampRepository lampRepository;

        public UpdateLampCommand(ILampRepository lampRepository)
        {
            this.lampRepository = lampRepository;
        }
        public void Excute(Guid id, string name, int brightness)
        {
            var lamp = lampRepository.GetById(id);
            if (lamp == null)
            {
                throw new Exception("Lamp not found");
            }
            lamp.Update(name, brightness);
        }
    }
}
