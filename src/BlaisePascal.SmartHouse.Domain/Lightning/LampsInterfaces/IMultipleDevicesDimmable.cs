using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lightning.LampsInterfaces
{
    public interface IMultipleDevicesDimmable
    {
        void SetSameBrightness(Brightness brightness);
    }
}
