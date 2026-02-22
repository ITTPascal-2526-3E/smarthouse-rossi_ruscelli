using BlaisePascal.SmartHouse.Domain.Lamps.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Lightning.Lamps.Commands
{
    public class SwitchLampOffCommand
    {
            private readonly ILampRepository lampRepository;
    
            public SwitchLampOffCommand(ILampRepository lampRepository)
            {
                this.lampRepository = lampRepository;
            }
    
            public void Execute(Guid id)
            {
                var lamp = lampRepository.GetById(id);
                if (lamp != null)
                {
                    lamp.SwitchOff();
                    lampRepository.Update(lamp);
                }
        }
    }
}
