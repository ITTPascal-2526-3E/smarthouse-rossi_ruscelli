using BlaisePascal.SmartHouse.Domain.Lamps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps.LampsInterfaces
{

    public interface IColorChange
    {
        void ChangeColor(ColorType newColor);
    }
}
