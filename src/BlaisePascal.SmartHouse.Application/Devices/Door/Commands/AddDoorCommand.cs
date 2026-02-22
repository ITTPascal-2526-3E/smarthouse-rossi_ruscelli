using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using BlaisePascal.SmartHouse.Domain.Door.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Door.Commands
{
    public class AddDoorCommand
    {
        private readonly IDoorRepository doorRepository;

        public AddDoorCommand(IDoorRepository doorRepository)
        {
            this.doorRepository = doorRepository;
        }

        public void Execute(NameDevice name, bool isLocked, bool isOpen)
        {
            var door = new Domain.Door.Door(isLocked, isOpen, name);
            doorRepository.Add(door);
        }
    }
}
