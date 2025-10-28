using System;

namespace BlaisePascal.SmartHouse.Domain
{
    internal class Lamp
    {
        private float powerConsumption; // Current power consumption in watts
        private bool IsOn; // State of the lamp
        private int Brightness; // Brightness of ther lamp
        private string Color; // Color of the lamp light
        private string Name; // Name of the lamp
        private int MaxConsumption; // Maximum power consumption in watts

        /// <summary>
        /// Propertys for Lamp class
        /// </summary>
        public bool IsOnProperty
        {
            get { return IsOn; }
            set { IsOn = value; }
        }
        public int BrightnessProperty
        {
            get { return Brightness; }
            set { Brightness = value; }
        }
        public string ColorProperty
        {
            get { return Color; }
            set { Color = value; }
        }
        public string NameProperty
        {
            get { return Name; }
            set { Name = value; }
        }
        public int MaxConsumptionProperty
        {
            get { return MaxConsumption; }
            set { MaxConsumption = value; }
        }
        public float PowerConsumption
        {
            get { return MaxConsumption * (Brightness / 100); } // Return power consumption based on brightness
            set { powerConsumption = value; }
        }

        /// <summary>
        /// Constructor for Lamp class
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        /// <param name="maxConsumption"></param>
        public void LampConstructor(bool isOn, string name, string color, int brightness, int maxConsumption)
        {
            IsOn = isOn;
            Name = name;
            Color = color;
            Brightness = brightness;
            MaxConsumption = maxConsumption;
        }

        // Turn on the lamp
        public void TurnOn()
        {
            IsOn = true;
        }

        // Turn off the lamp
        public void TurnOff()
        {
            IsOn = false;
        }

        // Change the brightness of the lamp
        public void ChangeBrightness(int newBrightness)
        {
            Brightness = newBrightness;
        }

        // Change the color of the lamp
        public void ChangeColor(string newColor)
        {
            Color = newColor;
        }


    }
}