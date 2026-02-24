using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using BlaisePascal.SmartHouse.Domain.Lightning.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Lightning.Lamps.Commands
{
    public class ChangeBrighntessCommand
    {
            private readonly ILampRepository lampRepository;
    
            public ChangeBrighntessCommand(ILampRepository lampRepository)
            {
                this.lampRepository = lampRepository;
            }
    
            public void Execute(Guid id, Brightness brightness)
            {
                var lamp = lampRepository.GetById(id);
                if (lamp != null)
                {
                    lamp.ChangeBrightness(brightness);
                    lampRepository.Remove(lamp);
                }
            }
    }
}
