using BlaisePascal.SmartHouse.Domain.Lightning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lightning.LampsInterfaces
{
    public interface ILedMatrix
    {
        Lamp[] GetLampsInColumn(int column);
        Lamp[] GetLampsInRow(int row);
        Lamp GetLamp(int row, int column);
    }
}
