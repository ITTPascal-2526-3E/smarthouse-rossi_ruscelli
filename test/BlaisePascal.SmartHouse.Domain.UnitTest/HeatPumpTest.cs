using System;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Heat_Pump;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class HeatPumpTest
    {
        [Fact]
        public void ConstructorAndProperties_ShouldInitializeCorrectly()
        {
            var pump = new HeatPump(isOn: false, temperature: 20.0, mode: EnumHeatPumpMode.HeatPumpMode.Heating, costPerKWh: 0.25f);

            Assert.False(pump.IsOnProperty);
            Assert.Equal(20.0, pump.TemperatureProperty);
            Assert.Equal(EnumHeatPumpMode.HeatPumpMode.Heating, pump.ModeProperty);
            Assert.Equal(0.25f, pump.CostPerKWhProperty);
        }

        [Fact]
        public void TurnOnOff_ShouldToggleState()
        {
            var pump = new HeatPump(isOn: false, temperature: 20.0, mode: EnumHeatPumpMode.HeatPumpMode.Eco, costPerKWh: 0.3f);

            pump.TurnOn();
            Assert.True(pump.IsOnProperty);

            pump.TurnOff();
            Assert.False(pump.IsOnProperty);
        }

        [Fact]
        public void ChangeMode_ShouldUpdateModeAndTemperatureBounds()
        {
            var pump = new HeatPump(isOn: true, temperature: 22.0, mode: EnumHeatPumpMode.HeatPumpMode.Heating, costPerKWh: 0.2f);

            pump.ChangeMode(EnumHeatPumpMode.HeatPumpMode.HotWater);
            Assert.Equal(EnumHeatPumpMode.HeatPumpMode.HotWater, pump.ModeProperty);

            // Prova a impostare una temperatura fuori dai limiti di HotWater (min 35.0, max 60.0) - dovrebbe essere ignorata
            pump.SetTemperature(30.0);
            Assert.NotEqual(30.0, pump.TemperatureProperty);
    
            // Imposta all'interno dei limiti
            pump.SetTemperature(40.0);
            Assert.Equal(40.0, pump.TemperatureProperty);
        }

        [Fact]
        public void TemperatureProperty_ShouldRespectModeBounds()
        {
            var pump = new HeatPump(isOn: true, temperature: 20.0, mode: EnumHeatPumpMode.HeatPumpMode.Comfort, costPerKWh: 0.18f);

            // I limiti di Comfort sono 20.0 - 26.0 per ModeProperties
            pump.TemperatureProperty = 19.0; // sotto il min -> ignorato
            Assert.Equal(20.0, pump.TemperatureProperty);

            pump.TemperatureProperty = 27.0; // sopra il max -> ignorato
            Assert.Equal(20.0, pump.TemperatureProperty);

            pump.TemperatureProperty = 22.5; // entro il range -> accettato
            Assert.Equal(22.5, pump.TemperatureProperty);
        }

        [Fact]
        public void CurrentConsumptionProperty_ShouldReturnZeroWhenOff()
        {
            var pump = new HeatPump(isOn: false, temperature: 22.0, mode: EnumHeatPumpMode.HeatPumpMode.Heating, costPerKWh: 0.2f);
            Assert.Equal(0, pump.CurrentConsumptionProperty);
        }

        [Fact]
        public void CurrentConsumptionProperty_ShouldComputeBetweenMinAndMaxWhenOn()
        {
            var pump = new HeatPump(isOn: true, temperature: 0.5, mode: EnumHeatPumpMode.HeatPumpMode.Heating, costPerKWh: 0.2f);

            // Per Heating: min 1200, max 2500. Formula nel codice: min + (max-min)*Temperature
            // Con Temperature = 0.5 => 1200 + 1300*0.5 = 1200 + 650 = 1850
            var consumption = pump.CurrentConsumptionProperty;
            Assert.Equal(1850, (int)Math.Round(consumption));
        }

        [Fact]
        public void StaticGetters_ShouldReturnExpectedValues()
        {
            Assert.Equal(2500, HeatPump.GetMaxConsumption(EnumHeatPumpMode.HeatPumpMode.Heating));
            Assert.Equal(1200, HeatPump.GetMinConsumption(EnumHeatPumpMode.HeatPumpMode.Heating));
            Assert.Equal(16.0, HeatPump.GetminTemperature(EnumHeatPumpMode.HeatPumpMode.Heating));
            Assert.Equal(30.0, HeatPump.GetmaxTemperature(EnumHeatPumpMode.HeatPumpMode.Heating));
        }
    }
}
