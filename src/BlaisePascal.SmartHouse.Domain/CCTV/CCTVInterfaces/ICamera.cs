using BlaisePascal.SmartHouse.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.CCTV.CCTVInterfaces
{
    public interface ICamera :  Iswitch
    {
        void StartRecording();
        void StopRecording();
    }
}
