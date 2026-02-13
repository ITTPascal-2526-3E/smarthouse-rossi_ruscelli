using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.Lamps.LampsInterfaces
{
    public interface IDimmable
    {
        void ChangeBrightness(Brightness brightness);
    }
}
