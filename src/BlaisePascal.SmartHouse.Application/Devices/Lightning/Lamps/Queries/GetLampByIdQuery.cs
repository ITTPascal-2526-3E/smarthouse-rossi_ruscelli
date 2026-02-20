using BlaisePascal.SmartHouse.Domain.Lamps;
using BlaisePascal.SmartHouse.Domain.Lamps.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Lightning.Lamps.Queries
{
    public class GetLampByIdQuery
    {
        private readonly ILampRepository lampRepository;
        public GetLampByIdQuery(ILampRepository lampRepository)
        {
            this.lampRepository = lampRepository;
        }
        public Lamp Execute(Guid id)
        {
            return lampRepository.GetById(id);
        }
    }
}
