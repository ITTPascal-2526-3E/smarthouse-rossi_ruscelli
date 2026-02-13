using BlaisePascal.SmartHouse.Domain.Interfaces;
using BlaisePascal.SmartHouse.Domain.Lamps;
using BlaisePascal.SmartHouse.Domain.Lamps.LampsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;


namespace BlaisePascal.SmartHouse.Domain.Abstractions
{
    
        public abstract class AbstractLamp : AbstractDevice, Iswitch, IDimmable, IColorChange, IChangeLampType
    {
            protected float powerConsumption; // Current power consumption in watts
            protected bool IsOn; // State of the lamp
            protected Brightness Brightness; // Brightness of ther lamp
            protected ColorType Color; // Color of the lamp light
            protected NameDevice Name; // Name of the lamp
            protected int MaxConsumption; // Maximum power consumption in watts
            protected LampType lampType;  // type of the lamp
            protected DateTime TurnedOnAt; // Time when the lamp was turned on
            protected DateTime TurnedOffAt; // Time when the lamp was turned off

            public AbstractLamp(bool isOn, NameDevice name, ColorType color, Brightness brightness, LampType lampType) : base(name)
        {

                Id = Guid.NewGuid();
                IsOn = isOn;
                
                Color = color;
                Brightness = brightness;
                this.lampType = lampType;
                if (isOn == true)
                { TurnedOnAt = DateTime.Now; }
                else
                { TurnedOffAt = DateTime.Now; }

                // set MaxConsumption based on the lampType using the dictionary
                MaxConsumption = GetMaxConsumption(lampType);

                // synchronize public properties with internal fields so tests and callers see consistent state
                LampTypeProperty = lampType;
                IsOnProperty = isOn;
                BrightnessProperty = brightness.Value;
                ColorProperty = color;
                NameProperty = name.Value;
            }

            private static readonly Dictionary<LampType, (ConsumptionDevice maxConsumption, float alpha)> lampTypeProperties = new()
        {
            { LampType.LED, (new ConsumptionDevice(25), 0.2f) },
            { LampType.CFL, (new ConsumptionDevice(40), 0.4f) },
            { LampType.Halogen, (new ConsumptionDevice(150), 0.8f) },
            { LampType.Incandescent, (new ConsumptionDevice(300), 1.0f) },
            { LampType.FluorescentLinear, (new ConsumptionDevice(80), 0.35f) },
            { LampType.HighPressureSodium, (new ConsumptionDevice(400), 0.25f) },
            { LampType.Induction, (new ConsumptionDevice(200), 0.30f) },
            { LampType.VintageLED, (new ConsumptionDevice(10), 0.25f) }
        };


            public float PowerConsumption
            {
                get
                {
                    int ValueBrightness = Brightness.Value;
                float alpha = lampTypeProperties[lampType].alpha;
                    if (IsOn == false) return 0;
                    return MaxConsumption * (ValueBrightness / 100.0f) * alpha;
                }

            }

            /// <summary>
            /// Constructor for Lamp class
            /// </summary>
            /// <param name="isOn"></param>
            /// <param name="name"></param>
            /// <param name="color"></param>
            /// <param name="brightness"></param>


            public static int GetConsumption(LampType lampType)
            {
                return lampTypeProperties[lampType].maxConsumption.Consumption;
            }

            // Added to match usage in tests and other classes
            public static int GetMaxConsumption(LampType lampType)
            {
                return lampTypeProperties[lampType].maxConsumption.Consumption;
            }

            public static float GetAlpha(LampType lampType)
            {
                return lampTypeProperties[lampType].alpha;
            }
            /// <summary>
            /// Propertys for Lamp class
            /// </summary>

            public LampType LampTypeProperty { get; private set; }

            public bool IsOnProperty { get; private set; }

            public int BrightnessProperty { get; private set; }

            public ColorType ColorProperty { get; private set; }
            public string NameProperty { get; set; }
            // Turn on the lamp
            public virtual void TurnOn()
            {
                if (!IsOn)
                {
                    IsOn = true;
                    IsOnProperty = true;
                    TurnedOnAt = DateTime.Now;
                }
            }

            // Turn off the lamp
            public virtual void TurnOff()
            {
                if (IsOn)
                {
                    IsOn = false;
                    IsOnProperty = false;
                    TurnedOffAt = DateTime.Now;
                }
            }

            // Change the brightness of the lamp
            public virtual void ChangeBrightness(Brightness newBrightness)
            {
            
                    Brightness= newBrightness;
            }

            // Change the color of the lamp
            public virtual void ChangeColor(ColorType newColor)
            {
                Color = newColor;
                ColorProperty = newColor;
            }

            public virtual void ChangeLampType(LampType newLampType)
            {
                lampType = newLampType;
                LampTypeProperty = newLampType;
                MaxConsumption = GetMaxConsumption(newLampType);
            }
        }
}
