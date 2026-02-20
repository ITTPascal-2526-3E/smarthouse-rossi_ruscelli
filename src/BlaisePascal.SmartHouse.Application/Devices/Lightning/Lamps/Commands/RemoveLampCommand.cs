using BlaisePascal.SmartHouse.Domain.Lamps.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Lightning.Lamps.Commands
{
    public class RemoveLampCommand
    {
        private readonly ILampRepository lampRepository;

        public RemoveLampCommand(ILampRepository lampRepository)
        {
            this.lampRepository = lampRepository;
        }

        
    }
}
