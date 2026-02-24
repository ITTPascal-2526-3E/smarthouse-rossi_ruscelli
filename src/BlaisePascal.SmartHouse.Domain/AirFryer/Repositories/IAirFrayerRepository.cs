using BlaisePascal.SmartHouse.Domain.AirFryerDevice;

namespace BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories
{
    public interface IAirFryerRepository
    {
        AirFryer GetById(Guid id);
        void Update(AirFryer airFryer);
        void Add(AirFryer airFryer);
        void TurnOn(AirFryer airFryer);
        void TurnOff(AirFryer airFryer);
        void SetTemp(AirFryer airFryer, int temperature);
        float GetMaxConsumption(AirFryer airFryer);
        float GetMinConsumption(AirFryer airFryer);
        void ChangeCost(AirFryer airFryer, float cost);
        float GetConsumption(AirFryer airFryer);
        void SelectMode(AirFryer airFryer, Mode mode);
        TimeSpan TimeOn(AirFryer airFryer);
        void SetTimer(AirFryer airFryer, TimeSpan timer);
        double ConnsumptionWattHours(AirFryer airFryer);
        double ConsumptionKiloWattHours(AirFryer airFryer);
        void Remove(AirFryer airFryer);
        List<AirFryer> GetAll();
    }
}