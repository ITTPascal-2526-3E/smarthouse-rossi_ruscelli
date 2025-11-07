using System;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class Lamp
    {
        private float powerConsumption; // Current power consumption in watts
        private bool IsOn; // State of the lamp
        private int Brightness; // Brightness of ther lamp
        private ColorType Color; // Color of the lamp light
        private string Name; // Name of the lamp
        private int MaxConsumption; // Maximum power consumption in watts
        private LampType lampType;

        
        
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
        public LampType LampTypeProperty
        {
            get { return lampType; }
            set { lampType = value; }
        }
        public bool IsOnProperty
        {
            get { return IsOn; }
            set { IsOn = value; }
        }
        public int BrightnessProperty
        {
            get { return Brightness; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    Brightness = value;
                }
                else
                {
                    Console.WriteLine("Brightness must be between 0 and 100.");
                }
            }
        }
        public ColorType ColorProperty
        {
            get { return Color; }
            set { Color = value; }
        }
        public string NameProperty
        {
            get { return Name; }
            set { Name = value; }
        }
        public float PowerConsumption /// Current power consumption in watts(calculated based on brightness and lamp type)
        {
            get
            {
                float alpha = lampTypeProperties[lampType].alpha;
                if (IsOn==false) return 0;
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
        public Lamp(bool isOn, string name, ColorType color, int brightness, LampType lampType)
        {
            IsOn = isOn;
            Name = name;
            Color = color;
            Brightness = brightness;
            this.lampType = lampType;

            // MaxConsumption viene automaticamente impostato dall'enum
            MaxConsumption = GetMaxConsumption(lampType);
        }

        // Turn on the lamp
        public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true;
                Console.WriteLine($"Lamp turned on at {DateTime.Now}.");
            }
        }

        // Turn off the lamp
        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false;
                Console.WriteLine($"Lamp turned off at {DateTime.Now}.");
            }
        }

        // Change the brightness of the lamp
        public void ChangeBrightness(int newBrightness)
        {
            if (newBrightness >= 0 && newBrightness <= 100)
            {
                Brightness = newBrightness;
            }
            else
            {
                Console.WriteLine("Brightness must be between 0 and 100.");
            }
        }

        // Change the color of the lamp
        public void ChangeColor(ColorType newColor)
        {
            Color = newColor;
        }


    }
}