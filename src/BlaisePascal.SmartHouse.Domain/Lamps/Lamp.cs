using System;
using BlaisePascal.SmartHouse.Domain.Abstractions;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public sealed class Lamp : AbstractLamp
    {
        public Lamp(bool isOn, NameDevice name, ColorType color, Brightness brightness, LampType lampType) : base(isOn, name, color, brightness, lampType)
        {

            if (isOn == true)
            { TurnedOnAt = DateTime.Now; }
            else
            { TurnedOffAt = DateTime.Now; }

            MaxConsumption = GetMaxConsumption(lampType);
        }

        public void SwitchOff()
        {
            throw new NotImplementedException();
        }

        public void SwitchOn()
        {
            throw new NotImplementedException();
        }

        public void Update(string name, int brightness)
        {
            throw new NotImplementedException();
        }
    }
}