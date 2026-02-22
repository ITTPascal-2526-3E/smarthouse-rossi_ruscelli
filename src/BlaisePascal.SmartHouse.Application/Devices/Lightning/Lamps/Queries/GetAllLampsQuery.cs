using BlaisePascal.SmartHouse.Domain.Lamps;
using BlaisePascal.SmartHouse.Domain.Lamps.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Lightning.Lamps.Queries
{
    public class GetAllLampsQuery
    {
        private readonly ILampRepository lampRepository;
        public GetAllLampsQuery(ILampRepository lampRepository)
        {
            this.lampRepository = lampRepository;
        }
        public IEnumerable<Lamp> Execute()
        {
            return lampRepository.GetAll();
        }
    }
}
