using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.CCTV.Queries
{
    public class GetAllCCTVQuery
    {
        private readonly Domain.CCTV.Repositories.ICCTVRepository cctvRepository;
        public GetAllCCTVQuery(Domain.CCTV.Repositories.ICCTVRepository cctvRepository)
        {
            this.cctvRepository = cctvRepository;
        }

        public List<Domain.CCTV.CCTV> Execute()
        {
            return cctvRepository.GetAll();
        }
    }
}
