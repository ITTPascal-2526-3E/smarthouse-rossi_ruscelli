using BlaisePascal.SmartHouse.Domain.AirFryerDevice;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using Xunit;
using System;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class AirFryerTest
    {
        [Fact]
        public void ConstructorAndProperties_ShouldInitializeCorrectly()
        {
            // Arrange
            var temp = new TemperatureDevice(180);
            var maxTemp = new TemperatureDevice(200);
            bool isOn = true;
            var costPerKWh = new CostPerKWh(0.15f);
            var name = new NameDevice("TestAirFryer");
            var mode = Mode.frying;

            // Act
            AirFryer test = new AirFryer(temp, maxTemp, isOn, costPerKWh, name, mode);

            // Assert
            Assert.Equal(200, test.TempProperty.Value);
            Assert.Equal(200, test.MaxTempProperty.Value);
            Assert.Equal(isOn, test.IsOnProperty);
            Assert.Equal(0.15f, test.CostPerKWhProperty.Value);
            Assert.Equal(mode, test.ModeProperty);
        }

        [Fact]
        public void GetConsumption_WhenOff_ReturnsZero()
        {
            // Arrange
            var temp = new TemperatureDevice(100);
            var maxTemp = new TemperatureDevice(200);
            bool isOn = false;
            var costPerKWh = new CostPerKWh(0.15f);
            var name = new NameDevice("TestAirFryer");
            var mode = Mode.frying;

            var fryer = new AirFryer(temp, maxTemp, isOn, costPerKWh, name, mode);

            // Act
            var consumption = fryer.GetConsumption();

            // Assert
            Assert.Equal(0f, consumption);
        }

        [Fact]
        public void GetMaxAndMinConsumption_ReturnExpectedValues()
        {
            // Arrange
            var temp = new TemperatureDevice(150);
            var maxTemp = new TemperatureDevice(200);
            bool isOn = true;
            var costPerKWh = new CostPerKWh(0.20f);
            var name = new NameDevice("TestAirFryer");
            var mode = Mode.dehydrate; // expects min 250, max 1500 from ModeProperties

            var fryer = new AirFryer(temp, maxTemp, isOn, costPerKWh, name, mode);

            // Act
            var max = fryer.GetMaxConsumption();
            var min = fryer.GetMinConsumption();

            // Assert
            Assert.Equal(1500f, max);
            Assert.Equal(250f, min);
            Assert.Equal(1500, fryer.MaxConsumptionProperty.Consumption);
            Assert.Equal(250, fryer.MinConsumptionProperty.Consumption);
        }

        [Fact]
        public void GetConsumption_WhenOn_AtTargetTemperature_ReturnsMinConsumption()
        {
            // Arrange
            var temp = new TemperatureDevice(200);
            var maxTemp = new TemperatureDevice(200);
            bool isOn = true;
            var costPerKWh = new CostPerKWh(0.10f);
            var name = new NameDevice("TestAirFryer");
            var mode = Mode.frying; // min consumption 800

            var fryer = new AirFryer(temp, maxTemp, isOn, costPerKWh, name, mode);

            // Act
            var consumption = fryer.GetConsumption();

            // Assert
            Assert.Equal(800f, consumption);
        }
    }
}
