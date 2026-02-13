using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.AirFryerDevice;

namespace BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories
{
     public interface IAirFrayerRepository
    {
        void Add(AirFryer airFryer);   
        void Update(AirFryer airFryer);
        void Remove(AirFryer airFryer);
        AirFryer GetById(Guid id);
        List<AirFryer> GetAll();
    }
}
