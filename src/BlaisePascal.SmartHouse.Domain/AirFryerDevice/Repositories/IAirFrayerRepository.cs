using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories
{
     public interface IAirFrayerRepository
    {
        void Add(AirFrayer airFrayer);
        void Update(AirFrayer airFrayer);
        void Remove(AirFrayer airFrayer);
        AirFrayer GetById(Guid id);
        List<AirFrayer> GetAll();
    }
}
