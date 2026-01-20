using BlaisePascal.SmartHouse.Domain.Interfaces;
using BlaisePascal.SmartHouse.Domain.Lamps;
using BlaisePascal.SmartHouse.Domain.Lamps.LampsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlaisePascal.SmartHouse.Domain.Abstractions
{
    
        public abstract class AbstractLamp : AbstractDevice, Iswitch, IDimmable, IColorChange, IChangeLampType
    {
            protected float powerConsumption; // Current power consumption in watts
            protected bool IsOn; // State of the lamp
            protected int Brightness; // Brightness of ther lamp
            protected ColorType Color; // Color of the lamp light
            protected string Name; // Name of the lamp
            protected int MaxConsumption; // Maximum power consumption in watts
            protected LampType lampType;  // type of the lamp
            protected DateTime TurnedOnAt; // Time when the lamp was turned on
            protected DateTime TurnedOffAt; // Time when the lamp was turned off

            public AbstractLamp(bool isOn, string name, ColorType color, int brightness, LampType lampType) : base(name)
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
            }

            private static readonly Dictionary<LampType, (int maxConsumption, float alpha)> lampTypeProperties = new()
        {
            { LampType.LED, (25, 0.2f) },
            { LampType.CFL, (40, 0.4f) },
            { LampType.Halogen, (150, 0.8f) },
            { LampType.Incandescent, (300, 1.0f) },
            { LampType.FluorescentLinear, (80, 0.35f) },
            { LampType.HighPressureSodium, (400, 0.25f) },
            { LampType.Induction, (200, 0.30f) },
            { LampType.VintageLED, (10, 0.25f) }
        };


            public float PowerConsumption
            {
                get
                {
                    float alpha = lampTypeProperties[lampType].alpha;
                    if (IsOn == false) return 0;
                    return MaxConsumption * (Brightness / 100.0f) * alpha;
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
                return lampTypeProperties[lampType].maxConsumption;
            }

            // Added to match usage in tests and other classes
            public static int GetMaxConsumption(LampType lampType)
            {
                return lampTypeProperties[lampType].maxConsumption;
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
                    TurnedOnAt = DateTime.Now;
                }
            }

            // Turn off the lamp
            public virtual void TurnOff()
            {
                if (IsOn)
                {
                    IsOn = false;
                    TurnedOnAt = DateTime.Now;
                }
            }

            // Change the brightness of the lamp
            public virtual void ChangeBrightness(int newBrightness)
            {
                if (newBrightness >= 0 && newBrightness <= 100 && IsOn)
                {
                    Brightness = newBrightness;
                }

            }

            // Change the color of the lamp
            public virtual void ChangeColor(ColorType newColor)
            {
                Color = newColor;
            }

            public virtual void ChangeLampType(LampType newLampType)
            {
                lampType = newLampType;
                MaxConsumption = GetMaxConsumption(newLampType); // aggiungi questa linea
            }
        }
}
