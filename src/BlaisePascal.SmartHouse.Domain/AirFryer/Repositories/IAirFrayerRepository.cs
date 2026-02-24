using BlaisePascal.SmartHouse.Domain.AirFryerDevice;

namespace BlaisePascal.SmartHouse.Domain.AirFryerDevice.Repositories
{
    public interface IAirFryerRepository
    {
        object GetById(Guid id);
        void Add(AirFryer airFryer);
        void TurnOn(AirFryer airFryer);
        void TurnOff(AirFryer airFryer);
        void SetTemp(AirFryer airFryer, int temperature);
        int GetMaxConsumption(AirFryer airFryer);
        float GetMinConsumption(AirFryer airFryer);
        void ChangCost(AirFryer airFryer, float cost);
        int GetConsumption(AirFryer airFryer);
        void SelectMode(AirFryer airFryer, Mode mode);
        TimeSpan TimeOn(AirFryer airFryer);
        void SetTimer(AirFryer airFryer, TimeSpan timer);
        double ConnsumptionWattHours(AirFryer airFryer);
        double ConsumptionKiloWattHours(AirFryer airFryer);
        void Remove(AirFryer airFryer);
        List<AirFryer> GetAll();
        void Delete(object airFryer);
    }
}