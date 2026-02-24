using BlaisePascal.SmartHouse.Domain.Door.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Door.Queries
{
    public class GetDoorByIdQuery
    {
        private readonly IDoorRepository doorRepository;

        public GetDoorByIdQuery(IDoorRepository doorRepository)
        {
            this.doorRepository = doorRepository;
        }

        public Domain.Door.Door Execute(Guid id)
        {
            var door = doorRepository.GetById(id);
            if (door == null)
            {
                throw new Exception("Door not found");
            }
            return door;
        }
    }
}
