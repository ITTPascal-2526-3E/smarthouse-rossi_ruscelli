using System;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Heat_Pump;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class HeatPumpTest
    {
        [Fact]
        public void ConstructorAndProperties_ShouldInitializeCorrectly()
        {
            var pump = new HeatPump(isOn: false, temperature: new TemperatureDevice(20), mode: HeatPumpMode.Heating, costPerKWh: new CostPerKWh(0.25f), name: new NameDevice("Test"));

            Assert.False(pump.IsOnProperty);
            Assert.Equal(20, pump.TemperatureProperty.Value);
            Assert.Equal(HeatPumpMode.Heating, pump.ModeProperty);
            Assert.Equal(0.25f, pump.CostPerKWhProperty.Value);
        }

        [Fact]
        public void TurnOnOff_ShouldToggleState()
        {
            var pump = new HeatPump(isOn: false, temperature: new TemperatureDevice(20), mode: HeatPumpMode.Eco, costPerKWh: new CostPerKWh(0.3f), name: new NameDevice("Test"));

            pump.TurnOn();
            Assert.True(pump.IsOnProperty);

            pump.TurnOff();
            Assert.False(pump.IsOnProperty);
        }

        [Fact]
        public void ChangeMode_ShouldUpdateModeAndTemperatureBounds()
        {
            var pump = new HeatPump(isOn: true, temperature: new TemperatureDevice(22), mode: HeatPumpMode.Heating, costPerKWh: new CostPerKWh(0.2f), name: new NameDevice("Test"));

            pump.ChangeMode(HeatPumpMode.HotWater);
            Assert.Equal(HeatPumpMode.HotWater, pump.ModeProperty);

            // Prova a impostare una temperatura fuori dai limiti di HotWater (min 16, max 30) - dovrebbe essere ignorata
            pump.SetTemp(new TemperatureDevice(30));
            Assert.Equal(30, pump.TemperatureProperty.Value);

            // Imposta all'interno dei limiti
            pump.SetTemp(new TemperatureDevice(40));
            // 40 è fuori dai limiti; Setter ignora -> non cambia
            Assert.NotEqual(40, pump.TemperatureProperty.Value);
        }

        [Fact]
        public void TemperatureProperty_ShouldRespectModeBounds()
        {
            var pump = new HeatPump(isOn: true, temperature: new TemperatureDevice(20), mode: HeatPumpMode.Comfort, costPerKWh: new CostPerKWh(0.18f), name: new NameDevice("Test"));

            // I limiti di Comfort sono 16 - 30 per ModeProperties
            pump.TemperatureProperty = new TemperatureDevice(16); // sotto o al min -> accettato
            Assert.Equal(16, pump.TemperatureProperty.Value);

            // Imposta una temperatura fuori range -> ignorata
            pump.TemperatureProperty = new TemperatureDevice(40);
            Assert.NotEqual(40, pump.TemperatureProperty.Value);

            pump.TemperatureProperty = new TemperatureDevice(22);
            Assert.Equal(22, pump.TemperatureProperty.Value);
        }

        [Fact]
        public void CurrentConsumptionProperty_ShouldReturnZeroWhenOff()
        {
            var pump = new HeatPump(isOn: false, temperature: new TemperatureDevice(22), mode: HeatPumpMode.Heating, costPerKWh: new CostPerKWh(0.2f), name: new NameDevice("Test"));
            Assert.Equal(0, pump.CurrentConsumptionProperty);
        }

        [Fact]
        public void CurrentConsumptionProperty_ShouldComputeBetweenMinAndMaxWhenOn()
        {
            // Use a temperature value normalized to [0,1] expected by current formula; Temperature.Value used directly in formula in code
            var pump = new HeatPump(isOn: true, temperature: new TemperatureDevice(1), mode:HeatPumpMode.Heating, costPerKWh: new CostPerKWh(0.2f), name: new NameDevice("Test"));

            // For Heating: min 1200, max 2500. Formula: min + (max-min)*Temperature.Value
            // With Temperature.Value = 1 => 1200 + 1300*1 = 2500
            var consumption = pump.CurrentConsumptionProperty;
            Assert.Equal(2500, (int)Math.Round(consumption));
        }
    }
}
