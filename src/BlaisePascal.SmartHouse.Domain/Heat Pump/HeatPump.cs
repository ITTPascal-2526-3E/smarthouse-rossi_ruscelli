using BlaisePascal.SmartHouse.Domain.Abstractions;
using BlaisePascal.SmartHouse.Domain.Interfaces;
using System;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.Heat_Pump
{
    public sealed class HeatPump : AbstractDevice, IHeatPump, ITemperatureAdjustable
    {
        private bool IsOn; // State of the heat pump
        private TemperatureDevice Temperature; // Current temperature of the heat pump
        public HeatPumpMode Mode { get; private set; }
        // removed unused CurrentConsumption field
        private float CostPerKWh; // Cost per kWh in currency unit

        /// <summary>
        /// Dizionario che contiene per ogni modalità i consumi massimo e minimo.
        /// (min = mantenimento temperatura, max = riscaldamento o raffrescamento attivo)
        /// </summary>
        private static readonly Dictionary<HeatPumpMode, (ConsumptionDevice maxConsumption, ConsumptionDevice minConsumption, TemperatureDevice minTemperature, TemperatureDevice maxTemperature)> ModeProperties = new()
        {
            { HeatPumpMode.Heating,  (new ConsumptionDevice(2500), new ConsumptionDevice(1200), new TemperatureDevice(16), new TemperatureDevice(30)) },
            { HeatPumpMode.Cooling,  (new ConsumptionDevice(2200), new ConsumptionDevice(1000), new TemperatureDevice(16), new TemperatureDevice(3)) },
            { HeatPumpMode.HotWater, (new ConsumptionDevice(2000) , new ConsumptionDevice(800), new TemperatureDevice(16), new TemperatureDevice(30)) },
            { HeatPumpMode.Eco,      (new ConsumptionDevice(1500) , new ConsumptionDevice(700), new TemperatureDevice(16), new TemperatureDevice(30)) },
            { HeatPumpMode.Comfort,  (new ConsumptionDevice(2600) , new ConsumptionDevice(1500),new TemperatureDevice(16), new TemperatureDevice(30)) },
            { HeatPumpMode.Auto,     (new ConsumptionDevice(2300) , new ConsumptionDevice(1000), new TemperatureDevice(16), new TemperatureDevice(30)) }
        };
        public int GetMaxConsumption()
        {
            return ModeProperties[Mode].maxConsumption.Consumption;
        }

        public int GetMinConsumption()
        {
            return ModeProperties[Mode].minConsumption.Consumption;
        }
        public TemperatureDevice GetminTemperature()
        {
            return ModeProperties[Mode].minTemperature;
        }
        public TemperatureDevice GetmaxTemperature()
        {
            return ModeProperties[Mode].maxTemperature;
        }

        /// <summary>
        /// Property heat pump.
        /// </summary>
        public bool IsOnProperty { get; private set; }
        public TemperatureDevice TemperatureProperty
        {
            get { return Temperature; }
            set
            {
                TemperatureDevice minTemp = GetminTemperature();
                TemperatureDevice maxTemp = GetmaxTemperature();
                if (value.Value >= minTemp.Value && value.Value <= maxTemp.Value)
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
                    double currentConsumption = minConsumption + (maxConsumption - minConsumption) * Temperature.Value;
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
        public HeatPump(bool isOn, TemperatureDevice temperature, HeatPumpMode mode, float costPerKWh, NameDevice name) : base(name)
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
        public void SetTemp(TemperatureDevice newTemperature)
        {
            TemperatureDevice minTemp = GetminTemperature();
            TemperatureDevice maxTemp = GetmaxTemperature();
            if (newTemperature.Value >= minTemp.Value && newTemperature.Value <= maxTemp.Value)
            {
                Temperature = newTemperature;
            }
        }
    }
}