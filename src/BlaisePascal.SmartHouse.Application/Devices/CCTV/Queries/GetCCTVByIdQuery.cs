using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.CCTV.Queries
{
    public class GetCCTVByIdQuery
    {
        private readonly Domain.CCTV.Repositories.ICCTVRepository cctvRepository;
        public GetCCTVByIdQuery(Domain.CCTV.Repositories.ICCTVRepository cctvRepository)
        {
            this.cctvRepository = cctvRepository;
        }
        public Domain.CCTV.CCTV Execute(Guid id)
        {
            var cctv = cctvRepository.GetById(id);
            if (cctv == null)
            {
                throw new Exception("CCTV not found");
            }
            return cctv;
        }
    }
}
