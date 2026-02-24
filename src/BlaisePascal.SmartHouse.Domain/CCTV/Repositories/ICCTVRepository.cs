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
        void TurnOn(CCTV cctv);
        void TurnOff(CCTV cctv);
        void StopRecording(CCTV cctv, bool isRecording);
        void StartRecording(CCTV cctv, bool isRecording);
        CCTV GetById(Guid id);
        List<CCTV> GetAll();
        void Delete(CCTV cctv);
    }
}
