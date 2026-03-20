using System;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Lightning;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class EcoLampTest
    {
        [Fact]
        public void PowerConsumption_WhenOff_ReturnsZero()
        {
            var lamp = new EcoLamp(false, new NameDevice("test"), ColorType.Red, new Brightness(50), LampType.LED);

            Assert.Equal(0f, lamp.PowerConsumption);
        }

        [Fact]
        public void PowerConsumption_WhenOn_IsCalculatedCorrectly()
        {
            var lamp = new EcoLamp(true, new NameDevice("test"), ColorType.Red, new Brightness(50), LampType.LED);

            float expected = EcoLamp.GetMaxConsumption(LampType.LED).Consumption * (50f / 100f) * EcoLamp.GetAlpha(LampType.LED);
            float actual = lamp.PowerConsumption;

            Assert.True(Math.Abs(expected - actual) < 0.0001f, $"expected {expected} actual {actual}");
        }

        [Fact]
        public void ChangeEcoMode_LimitsBrightnessWhenInEcoWindow()
        {
            // inizia con la lampada accesa e luminosità 80
            var lamp = new EcoLamp(true, new NameDevice("ecoTest"), ColorType.Red, new Brightness(80), LampType.LED);

            var now = DateTime.Now;
            TimeOnly start = TimeOnly.FromDateTime(now.AddHours(-1));
            TimeOnly end = TimeOnly.FromDateTime(now.AddHours(1));

            lamp.ChangeEcoMode(true, start, end, 40);

            Assert.True(lamp.BrightnessProperty.Value <= 40, $"Brightness should be limited to 40 but was {lamp.BrightnessProperty.Value}");
        }

        [Fact]
        public void StaticHelpers_ReturnExpectedValues()
        {
            // verify static helpers on AbstractLamp via EcoLamp
            var max = EcoLamp.GetMaxConsumption(LampType.LED);
            var alpha = EcoLamp.GetAlpha(LampType.LED);

            Assert.Equal(25, max.Consumption);
            Assert.Equal(0.2f, alpha);
        }
    }
}