using BlaisePascal.SmartHouse.Domain.Lightning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps
{
    public class InMemoryLampRepository
    {
        private readonly List<Lamp> _lamps;

        public InMemoryLampRepository()
        {
            _lamps = new List<Lamp>
            {
                new Lamp(true,new NameDevice("ciao1"),ColorType.WarmWhite,new Brightness(100),LampType.LED),
                new Lamp(true,new NameDevice("ciao2"),ColorType.WarmWhite,new Brightness(10),LampType.LED),
                new Lamp(false,new NameDevice("ciao3"),ColorType.WarmWhite,new Brightness(21),LampType.LED)
            };
        }

        public List<Lamp> GetAll()
        {
            return _lamps;
        }

        public Lamp? GetById(Guid id)
        {
            return _lamps.FirstOrDefault(l => l.Idproperty == id);
        }
    }
}
