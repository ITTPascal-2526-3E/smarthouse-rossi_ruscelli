using BlaisePascal.SmartHouse.Domain.AirFryer;
using System;

namespace BlaisePascal.SmartHouse.Domain.Heat_Pump
{
    internal class HeatPump
    {
        private bool IsOn; // State of the heat pump
        private double CurrentTemperature; // Current temperature of the heat pump
        private EnumHeatPumpMode.HeatPumpMode Mode; // Mode of the heat pump
        private int CurrentConsumption; // Current power consumption in watts
        private float CostPerKWh; // Cost per kWh in currency unit

        /// <summary>
        /// Dizionario che contiene per ogni modalità i consumi massimo e minimo.
        /// (min = mantenimento temperatura, max = riscaldamento o raffrescamento attivo)
        /// </summary>
        private static readonly Dictionary<EnumHeatPumpMode.HeatPumpMode, (int maxConsumption, int minConsumption)> ModeProperties = new()
        {
            { EnumHeatPumpMode.HeatPumpMode.Heating,  (2500, 1200) },
            { EnumHeatPumpMode.HeatPumpMode.Cooling,  (2200, 1000) },
            { EnumHeatPumpMode.HeatPumpMode.HotWater, (2000,  800) },
            { EnumHeatPumpMode.HeatPumpMode.Eco,      (1500,  700) },
            { EnumHeatPumpMode.HeatPumpMode.Comfort,  (2600, 1500) },
            { EnumHeatPumpMode.HeatPumpMode.Auto,     (2300, 1000) }
        };

        /// <summary>
        /// Property heat pump.
        /// </summary>
        public bool IsOnProperty
        {
            get { return IsOn; }
            set { IsOn = value; }
        }
        public double CurrentTemperatureProperty
        {
            get { return CurrentTemperature; }
            set { CurrentTemperature = value; }
        }
        public EnumHeatPumpMode.HeatPumpMode ModeProperty
        {
            get { return Mode; }
            set { Mode = value; }
        }
        public float CostPerKWhProperty
        {
            get { return CostPerKWh; }
            set { CostPerKWh = value; }
        }

        /// <summary>
        /// Constructor for HeatPump class
        /// </summary>
        public HeatPump(bool isOn, double currentTemperature, EnumHeatPumpMode.HeatPumpMode mode, float costPerKWh)
        {
            IsOn = isOn;
            CurrentTemperature = currentTemperature;
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
    }
}
