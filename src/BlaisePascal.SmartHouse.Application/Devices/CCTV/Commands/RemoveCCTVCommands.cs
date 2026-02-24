using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.CCTV.Commands
{
    public class RemoveCCTVCommands
    {
        private readonly Domain.CCTV.Repositories.ICCTVRepository cctvRepository;
        public RemoveCCTVCommands(Domain.CCTV.Repositories.ICCTVRepository cctvRepository)
        {
            this.cctvRepository = cctvRepository;
        }
    }

    public class RemoveCCTVCommand
    {
        private readonly Domain.CCTV.Repositories.ICCTVRepository cctvRepository;
        public RemoveCCTVCommand(Domain.CCTV.Repositories.ICCTVRepository cctvRepository)
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
            cctvRepository.Remove(cctv);
        }
    }
}
