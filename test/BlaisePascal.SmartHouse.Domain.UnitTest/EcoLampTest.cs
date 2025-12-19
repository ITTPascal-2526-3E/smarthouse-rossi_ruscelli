using System;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Lamps;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class EcoLampTest
    {
        [Fact]
        public void PowerConsumption_WhenOff_ReturnsZero()
        {
            var lamp = new EcoLamp(false, "test", ColorType.Red, 50, LampType.LED);

            Assert.Equal(0f, lamp.PowerConsumption);
        }

        [Fact]
        public void PowerConsumption_WhenOn_IsCalculatedCorrectly()
        {
            var lamp = new EcoLamp(true, "test", ColorType.Red, 50, LampType.LED);

            float expected = EcoLamp.GetMaxConsumption(LampType.LED) * (50f / 100f) * EcoLamp.GetAlpha(LampType.LED);
            float actual = lamp.PowerConsumption;

            Assert.True(Math.Abs(expected - actual) < 0.0001f, $"expected {expected} actual {actual}");
        }


        [Fact]
        public void ChangeEcoMode_LimitsBrightnessWhenInEcoWindow()
        {
            // inizia con la lampada accesa e luminosità 80
            var lamp = new EcoLamp(true, "ecoTest", ColorType.Red, 80, LampType.LED);

            var now = DateTime.Now;
            TimeOnly start = TimeOnly.FromDateTime(now.AddHours(-1));
            TimeOnly end = TimeOnly.FromDateTime(now.AddHours(1));

            lamp.ChangeEcoMode(true, start, end, 40);

            Assert.True(lamp.BrightnessProperty <= 40, $"Brightness should be limited to 40 but was {lamp.BrightnessProperty}");
        }
    }
}