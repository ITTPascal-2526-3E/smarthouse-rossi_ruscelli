using BlaisePascal.SmartHouse.Domain.AirFryer;
using BlaisePascal.SmartHouse.Domain.Heat_Pump;
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
    public interface  IGetConsumption
    {
        int GetConsumption();
    }
    public interface IGetDoubleConsumption
    {
        int GetMaxConsumption();
        int GetMinConsumption();
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
    public interface  I2DevicesDimmable
    {
        void SetSameBrightness(bool Which);
    }
    public interface ILedMatrix
    {
        Lamp[] GetLampsInColumn(int column);
        Lamp[] GetLampsInRow(int row);
        Lamp GetLamp(int row, int column);
    }
    public interface IMultipleDevices
    {
        void TurnOnAll();
        void TurnOffAll();
    }
    public interface  IMultipleDevicesDimmable
    {
        void SetSameBrightness(int brightness);
    }
    public interface ILockable
    {
        void Lock();
        void Unlock();
    }
    public interface  IOpenable
    {
        void Open();
        void Close();
    }
    public interface ITimerSettable
    {
        void SetTimer(TimeSpan time);
    }
    public interface ITemperatureAdjustable
    {
        void SetTemp(int temp);
    }
    public interface IGetDoubleTemperature
    {
        int GetMinConsumption();
        int GetminTemperature();
       
    }
    public interface IChangeableCost
    {
        void ChangeCost(float cost);
    }
    public interface ITimeElapsedOn
    {
        TimeSpan TimeOn();
    }
    public interface IModeSelectable
    {
        void SelectMode(Mode mode);
    }
    public interface IGetEnergyConsumption
    {
        double ConsumptionWattHours(Mode mode);
        double ConsumptionKiloWattHours(Mode mode);
    }
    public interface IGetCost
    {
        double CostOfConsumption(Mode mode);
    }
    public interface  IModeChangeHeatPump
    {
        void ChangeMode(HeatPumpMode newMode);
    }
}
