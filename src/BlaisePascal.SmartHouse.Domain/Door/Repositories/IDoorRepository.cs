using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Door.Repositories
{
    public interface IDoorRepository
    {
        void Add(Door door);
        void Update(Door door);
        void Remove(Door door);
        void Lock(Door door);
        void Unlock(Door door);
        void Open(Door door);
        void Close(Door door);
        Door GetById(Guid id);
        List<Door> GetAll();
    }
}
