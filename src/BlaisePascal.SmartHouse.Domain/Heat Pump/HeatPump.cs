using BlaisePascal.SmartHouse.Domain.Abstractions;
using BlaisePascal.SmartHouse.Domain.Interfaces;
using System;

namespace BlaisePascal.SmartHouse.Domain.Heat_Pump
{
    public sealed class HeatPump : AbstractDevice,IHeatPump, ITemperatureAdjustable
    {
        private bool IsOn; // State of the heat pump
        private double Temperature; // Current temperature of the heat pump
        public HeatPumpMode Mode { get; private set; }
        // removed unused CurrentConsumption field
        private float CostPerKWh; // Cost per kWh in currency unit

        /// <summary>
        /// Dizionario che contiene per ogni modalità i consumi massimo e minimo.
        /// (min = mantenimento temperatura, max = riscaldamento o raffrescamento attivo)
        /// </summary>
        private static readonly Dictionary<HeatPumpMode, (int maxConsumption, int minConsumption, int minTemperature, int maxTemperature)> ModeProperties = new()
        {
            { HeatPumpMode.Heating,  (2500, 1200, 16, 30) },
            { HeatPumpMode.Cooling,  (2200, 1000, 16 , 3) },
            { HeatPumpMode.HotWater, (2000,  800, 16 , 30) },
            { HeatPumpMode.Eco,      (1500,  700, 16 , 30) },
            { HeatPumpMode.Comfort,  (2600, 1500, 16 , 30) },
            { HeatPumpMode.Auto,     (2300, 1000, 16 , 30) }
        };
        public int GetMaxConsumption()
        {
            return ModeProperties[Mode].maxConsumption;
        }

        public int GetMinConsumption()
        {
            return ModeProperties[Mode].minConsumption;
        }
        public int GetminTemperature()
        {
            return ModeProperties[Mode].minTemperature;
        }
        public int GetmaxTemperature()
        {
            return ModeProperties[Mode].maxTemperature;
        }

        /// <summary>
        /// Property heat pump.
        /// </summary>
        public bool IsOnProperty { get; private set; }
        public double TemperatureProperty
        {
            get { return Temperature; }
            set
            {
                int minTemp = GetminTemperature();
                int maxTemp = GetmaxTemperature();
                if (value >= minTemp && value <= maxTemp)
                {
                    Temperature = value;
                }
            }
        }
        public HeatPumpMode ModeProperty
        {
            get { return Mode; }
            set { Mode = value; }
        }
        public double CurrentConsumptionProperty
        {
            get
            {
                int maxConsumption = GetMaxConsumption();
                int minConsumption = GetMinConsumption();
                if (IsOn)
                {
                    double currentConsumption = minConsumption + (maxConsumption - minConsumption) * Temperature;
                    return currentConsumption;
                }
                else
                {
                    return 0;

                }
            }
        }
        public float CostPerKWhProperty
        {
            get { return CostPerKWh; }
            set { CostPerKWh = value; }
        }

        /// <summary>
        /// Constructor for HeatPump class
        /// </summary>
        public HeatPump(bool isOn, double temperature, HeatPumpMode mode, float costPerKWh, string name) : base(name)
        {
            Id = Guid.NewGuid();
            IsOn = isOn;
            Temperature = temperature;
            Mode = mode;
            CostPerKWh = costPerKWh;
        }

        public void TurnOn()
        {
            if(!IsOn) 
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false;
            }
            
        }

        public void ChangeMode(HeatPumpMode newMode)
        {
            Mode = newMode;
        }
        public void SetTemp(int newTemperature)
        {
            int minTemp = GetminTemperature();
            int maxTemp = GetmaxTemperature();
            if (newTemperature >= minTemp && newTemperature <= maxTemp)
            {
                Temperature = newTemperature;
            }
        }
    }
}