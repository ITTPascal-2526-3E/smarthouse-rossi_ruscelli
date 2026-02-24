/*using System;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Heat_Pump;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class HeatPumpTest
    {
        [Fact]
        public void ConstructorAndProperties_ShouldInitializeCorrectly()
        {
            var pump = new HeatPump(isOn: false, temperature: 20.0, mode: HeatPumpMode.Heating, costPerKWh: 0.25f, name: "Test");

            Assert.False(pump.IsOnProperty);
            Assert.Equal(20.0, pump.TemperatureProperty);
            Assert.Equal(HeatPumpMode.Heating, pump.ModeProperty);
            Assert.Equal(0.25f, pump.CostPerKWhProperty);
        }

        [Fact]
        public void TurnOnOff_ShouldToggleState()
        {
            var pump = new HeatPump(isOn: false, temperature: 20.0, mode: HeatPumpMode.Eco, costPerKWh: 0.3f, name: "Test");

            pump.TurnOn();
            Assert.True(pump.IsOnProperty);

            pump.TurnOff();
            Assert.False(pump.IsOnProperty);
        }

        [Fact]
        public void ChangeMode_ShouldUpdateModeAndTemperatureBounds()
        {
            var pump = new HeatPump(isOn: true, temperature: 22.0, mode: HeatPumpMode.Heating, costPerKWh: 0.2f, name: "Test");

            pump.ChangeMode(HeatPumpMode.HotWater);
            Assert.Equal(HeatPumpMode.HotWater, pump.ModeProperty);

            // Prova a impostare una temperatura fuori dai limiti di HotWater (min 35.0, max 60.0) - dovrebbe essere ignorata
            pump.SetTemp(30);
            Assert.NotEqual(30, pump.TemperatureProperty);
    
            // Imposta all'interno dei limiti
            pump.SetTemp(40);
            Assert.Equal(40, pump.TemperatureProperty);
        }

        [Fact]
        public void TemperatureProperty_ShouldRespectModeBounds()
        {
            var pump = new HeatPump(isOn: true, temperature: 20.0, mode: HeatPumpMode.Comfort, costPerKWh: 0.18f, name: "Test");

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
            var pump = new HeatPump(isOn: false, temperature: 22.0, mode: HeatPumpMode.Heating, costPerKWh: 0.2f, name: "Test");
            Assert.Equal(0, pump.CurrentConsumptionProperty);
        }

        [Fact]
        public void CurrentConsumptionProperty_ShouldComputeBetweenMinAndMaxWhenOn()
        {
            var pump = new HeatPump(isOn: true, temperature: 0.5, mode:HeatPumpMode.Heating, costPerKWh: 0.2f, name: "Test");

            // Per Heating: min 1200, max 2500. Formula nel codice: min + (max-min)*Temperature
            // Con Temperature = 0.5 => 1200 + 1300*0.5 = 1200 + 650 = 1850
            var consumption = pump.CurrentConsumptionProperty;
            Assert.Equal(1850, (int)Math.Round(consumption));
        }
    }
}
*/