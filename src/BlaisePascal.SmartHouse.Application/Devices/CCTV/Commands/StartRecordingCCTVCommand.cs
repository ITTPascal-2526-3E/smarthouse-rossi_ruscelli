using BlaisePascal.SmartHouse.Domain.CCTV.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.CCTV.Commands
{
    public class StartRecordingCCTVCommand
    {
        private readonly ICCTVRepository cctvRepository;
        public StartRecordingCCTVCommand(ICCTVRepository cctvRepository)
        {
            this.cctvRepository = cctvRepository;
        }
        public void Execute(Guid id)
        {
            var cctv = cctvRepository.GetById(id);
            if (cctv == null)
            {
                throw new Exception("CCTV not found");
            }
            cctv.StartRecording();
        }
    }
}
