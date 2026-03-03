using BlaisePascal.SmartHouse.Domain.Lightning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps
{
    public class InMemoryLampRepository
    {
        private readonly List<Lamp> _lamps;

        public InMemoryLampRepository()
        {
            _lamps = new List<Lamp>
            {
                new Lamp(Guid.NewGuid(), "Living Room Lamp", 60, true),
                new Lamp(Guid.NewGuid(), "Bedroom Lamp", 40, false),
                new Lamp(Guid.NewGuid(), "Kitchen Lamp", 75, true)
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
