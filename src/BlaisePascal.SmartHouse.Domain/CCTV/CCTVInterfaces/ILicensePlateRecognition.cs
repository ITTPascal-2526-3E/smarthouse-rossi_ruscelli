using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.CCTV.CCTVInterfaces
{
    public interface ILicensePlateRecognition
    {
        void EnableLicensePlateRecognition(LicensePlate licensePlate);
    }
}
