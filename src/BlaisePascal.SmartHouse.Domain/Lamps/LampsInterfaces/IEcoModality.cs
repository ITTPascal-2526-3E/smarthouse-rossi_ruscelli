using BlaisePascal.SmartHouse.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps.LampsInterfaces
{
    public interface IEcoModality
    {
        bool IsInEco(DateTime dateTime);
        void ChangeEcoMode(bool enebled, TimeOnly start, TimeOnly end, int maxBrightness);
        void ChangeTimers(TimeSpan defaultAutoOff, TimeSpan ecoAutoOff);
        void TurnOnEco();
        DateTime NextOccurrence(DateTime from, TimeOnly time);
        DateTime? ComputeFinalOffInstant(DateTime TurnOnAt);
    }
}
