using BlaisePascal.SmartHouse.Domain.Lamps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlaisePascal.SmartHouse.Domain.Interfaces
{
    public interface Iswitch
    {
        void TurnOn();
        void TurnOff();
    }
    public interface IDimmable
    {
        void ChangeBrightness(int brightness);
    }
    public interface IColorChangable
    {
        void ChangeColor(ColorType newColor);
    }
    public interface IChangeLampType
    {
        void ChangeLampType(LampType newLampType);
    }
    public interface  IEcoModality
    {
        bool IsInEco(DateTime dateTime);
        void ChangeEcoMode(bool enebled, TimeOnly start, TimeOnly end, int maxBrightness);
        void ChangeTimers(TimeSpan defaultAutoOff, TimeSpan ecoAutoOff);
        void TurnOnEco();
        DateTime NextOccurrence(DateTime from, TimeOnly time);
        DateTime? ComputeFinalOffInstant(DateTime TurnOnAt);
    }
    public interface ILockable
    {
        void Lock();
        void Unlock();
    }
    public interface ISchedulable
    {
        void ScheduleTurnOn(DateTime dateTime);
        void ScheduleTurnOff(DateTime dateTime);
    }
    public interface ITemperatureAdjustable
    {
        void SetTemperature(double temperature);
    }
    
}
