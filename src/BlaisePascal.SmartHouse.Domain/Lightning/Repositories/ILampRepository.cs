using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lightning.Repositories
{
    public interface ILampRepository
    {
        void Add(Lamp lamp);
        void Remove(Lamp lamp);
        Lamp GetById(Guid id);
        List<Lamp> GetAll();
    }
}
