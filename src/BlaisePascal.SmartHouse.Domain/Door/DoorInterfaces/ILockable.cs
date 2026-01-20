using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Door.DoorInterfaces
{
    public interface ILockable
    {
        void Lock();
        void Unlock();
    }
}
