using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.CCTV.Commands
{
    public class AddCCTVCommand
    {
        private readonly Domain.CCTV.Repositories.ICCTVRepository cctvRepository;
        public AddCCTVCommand(Domain.CCTV.Repositories.ICCTVRepository cctvRepository)
        {
            this.cctvRepository = cctvRepository;
        }
    }
}
