using BlaisePascal.SmartHouse.Domain.Door.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Door.Queries
{
    public class GetAllDoorsQuery
    {
        private readonly IDoorRepository doorRepository;

        public GetAllDoorsQuery(IDoorRepository doorRepository)
        {
            this.doorRepository = doorRepository;
        }

        public Domain.Door.Door[] Execute()
        {
            return doorRepository.GetAll().ToArray();
        }
    }
}
