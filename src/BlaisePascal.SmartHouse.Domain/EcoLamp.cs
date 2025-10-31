using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace BlaisePascal.SmartHouse.Domain
{
    public class EcoLamp
    {
        private float powerConsumption; // Current power consumption in watts
        private bool IsOn; // State of the lamp
        private int Brightness; // Brightness of ther lamp
        private string Color; // Color of the lamp light
        private string Name; // Name of the lamp
        private int MaxConsumption; // Maximum power consumption in watts
        private DateTime EcoModeStartTime; // Start time of Eco mode
        private DateTime EcoModeEndTime; // End time of Eco mode
        private DateTime TimeTurnOff; // Time to turn off the lamp in Eco mode
        private Timer TimerTurnOff; // Timer for turning off the lamp
        private Timer TimerTurnOffEcoMode; // Timer for turning off the lamp in Eco mode
        /// <summary>
        /// Propertys for EcoLamp class
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
        /// Constructor for EcoLamp class
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        /// <param name="maxConsumption"></param>
        public void EcoLampConstructor(bool isOn, string name, string color, int brightness, int maxConsumption)
        {
            IsOn = isOn;
            Name = name;
            Color = color;
            Brightness = brightness;
            MaxConsumption = maxConsumption;


        }
    }
}
