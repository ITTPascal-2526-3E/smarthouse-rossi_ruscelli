using BlaisePascal.SmartHouse.Domain.AirFryer;
using System;

namespace BlaisePascal.SmartHouse.Domain.Heat_Pump
{
    internal class HeatPump
    {
        private bool IsOn; // State of the heat pump
        private double Temperature; // Current temperature of the heat pump
        private EnumHeatPumpMode.HeatPumpMode Mode; // Mode of the heat pump
        private int CurrentConsumption; // Current power consumption in watts
        private float CostPerKWh; // Cost per kWh in currency unit

        /// <summary>
        /// Dizionario che contiene per ogni modalità i consumi massimo e minimo.
        /// (min = mantenimento temperatura, max = riscaldamento o raffrescamento attivo)
        /// </summary>
        private static readonly Dictionary<EnumHeatPumpMode.HeatPumpMode, (int maxConsumption, int minConsumption, double minTemperature, double maxTemperature)> ModeProperties = new()
        {
            { EnumHeatPumpMode.HeatPumpMode.Heating,  (2500, 1200, 16.0, 30.0) },
            { EnumHeatPumpMode.HeatPumpMode.Cooling,  (2200, 1000, 18.0, 32.0) },
            { EnumHeatPumpMode.HeatPumpMode.HotWater, (2000,  800, 35.0, 60.0) },
            { EnumHeatPumpMode.HeatPumpMode.Eco,      (1500,  700, 18.0, 28.0) },
            { EnumHeatPumpMode.HeatPumpMode.Comfort,  (2600, 1500, 20.0, 26.0) },
            { EnumHeatPumpMode.HeatPumpMode.Auto,     (2300, 1000, 18.0, 30.0) }
        };
        public static int GetMaxConsumption(EnumHeatPumpMode.HeatPumpMode mode)
        {
            return ModeProperties[mode].maxConsumption;
        }

        public static int GetMinConsumption(EnumHeatPumpMode.HeatPumpMode mode)
        {
            return ModeProperties[mode].minConsumption;
        }
        public static double GetminTemperature(EnumHeatPumpMode.HeatPumpMode mode)
        {
            return ModeProperties[mode].minTemperature;
        }
        public static double GetmaxTemperature(EnumHeatPumpMode.HeatPumpMode mode)
        {
            return ModeProperties[mode].maxTemperature;
        }

        /// <summary>
        /// Property heat pump.
        /// </summary>
        public bool IsOnProperty
        {
            get { return IsOn; }
            set { IsOn = value; }
        }
        public double TemperatureProperty
        {
            get { return Temperature; }
            set
            {
                double minTemp = GetminTemperature(Mode);
                double maxTemp = GetmaxTemperature(Mode);
                if (value >= minTemp && value <= maxTemp)
                {
                    Temperature = value;
                }
            }
        }
        public EnumHeatPumpMode.HeatPumpMode ModeProperty
        {
            get { return Mode; }
            set { Mode = value; }
        }
        public double CurrentConsumptionProperty
        {
            get
            {
                int maxConsumption = GetMaxConsumption(Mode);
                int minConsumption = GetMinConsumption(Mode);
                if (IsOn)
                {
                    double corruntConsumption = minConsumption + (maxConsumption - minConsumption) * CurrentTemperature;
                    return corruntConsumption;
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
        public HeatPump(bool isOn, double Temperature, EnumHeatPumpMode.HeatPumpMode mode, float costPerKWh)
        {
            IsOn = isOn;
            Temperature = Temperature;
            Mode = mode;
            CostPerKWh = costPerKWh;
        }

        public void TurnOn()
        {
            IsOn = true;
        }

        public void TurnOff()
        {
            IsOn = false;
        }

        public void ChangeMode(EnumHeatPumpMode.HeatPumpMode newMode)
        {
            Mode = newMode;
        }
        public void SetTemperature(double newTemperature)
        {
            double minTemp = GetminTemperature(Mode);
            double maxTemp = GetmaxTemperature(Mode);
            if (newTemperature >= minTemp && newTemperature <= maxTemp)
            {
                Temperature = newTemperature;
            }
        }
    }
}
