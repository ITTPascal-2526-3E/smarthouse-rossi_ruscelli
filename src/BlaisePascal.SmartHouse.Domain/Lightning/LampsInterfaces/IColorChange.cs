using BlaisePascal.SmartHouse.Domain.Lightning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lightning.LampsInterfaces
{

    public interface IColorChange
    {
        void ChangeColor(ColorType newColor);
    }
}
