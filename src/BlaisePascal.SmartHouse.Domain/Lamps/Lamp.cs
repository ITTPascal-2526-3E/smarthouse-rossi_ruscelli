using System;
using BlaisePascal.SmartHouse.Domain.Abstractions;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public sealed class Lamp : AbstractLamp
    {
        public Lamp(bool isOn, string name, ColorType color, int brightness, LampType lampType) : base(isOn, name, color, brightness, lampType)
        {

            Id = Guid.NewGuid();
            IsOn = isOn;
            Name = name;
            Color = color;
            Brightness = brightness;
            this.lampType = lampType;
            if (isOn == true)
            { TurnedOnAt = DateTime.Now; }
            else
            { TurnedOffAt = DateTime.Now; }

            MaxConsumption = GetMaxConsumption(lampType);
        }
    }
}