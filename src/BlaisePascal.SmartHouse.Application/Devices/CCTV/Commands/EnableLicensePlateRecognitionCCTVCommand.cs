using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using BlaisePascal.SmartHouse.Domain.CCTV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.CCTV.Commands
{
    public class EnableLicensePlateRecognitionCCTVCommand
    {
        private readonly ICCTVRepository cctvRepository;

        public EnableLicensePlateRecognitionCCTVCommand(ICCTVRepository cctvRepository)
        {
            this.cctvRepository = cctvRepository;
        }

        public void Excute(Guid id,LicensePlate licensePlate)
        {
            var cctv = cctvRepository.GetById(id);
            if (cctv == null)
            {
                throw new Exception("CCTV not found");
            }
            cctv.EnableLicensePlateRecognition(licensePlate);
        }
    }
}
