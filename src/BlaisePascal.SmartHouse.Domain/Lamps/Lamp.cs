using System;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class Lamp
    {
        

        protected float powerConsumption; // Current power consumption in watts
        protected bool IsOn; // State of the lamp
        protected int Brightness; // Brightness of ther lamp
        protected ColorType Color; // Color of the lamp light
        protected string Name; // Name of the lamp
        protected int MaxConsumption; // Maximum power consumption in watts
        protected LampType lampType;
        protected DateTime TurnedOnAt; // Time when the lamp was turned on
        protected DateTime TurnedOffAt; // Time when the lamp was turned off
        protected Guid Id; // Unique identifier for the lamp


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
       
        public LampType LampTypeProperty { get; private set; }

        public bool IsOnProperty { get; private set; }

        public int BrightnessProperty { get; private set; }

        public ColorType ColorProperty { get; private set; }

        public int BrightnessLevel { get; private set; } // Brightness level from 0 to 100
        public string NameProperty { get; set; }

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
        public Lamp(bool isOn, string name, ColorType color, int brightness, LampType lampType)
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

        // Turn on the lamp
        public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true;
                TurnedOnAt=DateTime.Now;
            }
        }

        // Turn off the lamp
        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false;
                TurnedOnAt = DateTime.Now;
            }
        }

        // Change the brightness of the lamp
        public void ChangeBrightness(int newBrightness)
        {
            if (newBrightness >= 0 && newBrightness <= 100)
            {
                Brightness = newBrightness;
            }
            
        }

        // Change the color of the lamp
        public void ChangeColor(ColorType newColor)
        {
            Color = newColor;
        }


    }
}