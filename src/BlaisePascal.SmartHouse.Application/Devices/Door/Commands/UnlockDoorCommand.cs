using BlaisePascal.SmartHouse.Domain.Door.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Door.Commands
{
    public class UnlockDoorCommand
    {
        private readonly IDoorRepository doorRepository;
    
            public UnlockDoorCommand(IDoorRepository doorRepository)
            {
                this.doorRepository = doorRepository;
            }
    
            public void Execute(Guid id)
            {
                var door = doorRepository.GetById(id);
                if (door == null)
                {
                    throw new Exception("Door not found");
                }
                door.Unlock();
        }
    }
}
