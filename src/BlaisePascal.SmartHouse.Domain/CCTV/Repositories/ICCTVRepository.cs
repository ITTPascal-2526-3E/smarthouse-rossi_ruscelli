using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.CCTV.Repositories
{
    public interface ICCTVRepository
    {
        void Add(CCTV cctv);
        void Update(CCTV cctv);
        void Remove(CCTV cctv);
        CCTV GetById(Guid id);
        List<CCTV> GetAll();
    }
}
