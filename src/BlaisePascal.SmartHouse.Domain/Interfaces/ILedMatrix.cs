using BlaisePascal.SmartHouse.Domain.Lamps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Interfaces
{
    public interface ILedMatrix
    {
        Lamp[] GetLampsInColumn(int column);
        Lamp[] GetLampsInRow(int row);
        Lamp GetLamp(int row, int column);
    }
}
