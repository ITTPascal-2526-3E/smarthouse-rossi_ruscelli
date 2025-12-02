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
        public void BrightnessProperty_IsClampedTo0And100()
        {
            var lamp = new EcoLamp(true, "test", ColorType.Red, 50, LampType.LED);

            lamp.BrightnessProperty = 150;
            Assert.Equal(100, lamp.BrightnessProperty);

            lamp.BrightnessProperty = -10;
            Assert.Equal(0, lamp.BrightnessProperty);
        }

        [Fact]
        public void ChangeEcoMode_LimitsBrightnessWhenInEcoWindow()
        {
            // start with lamp on and brightness 80
            var lamp = new EcoLamp(true, "ecoTest", ColorType.Red, 80, LampType.LED);

            var now = DateTime.Now;
            TimeOnly start = TimeOnly.FromDateTime(now.AddHours(-1));
            TimeOnly end = TimeOnly.FromDateTime(now.AddHours(1));

            lamp.ChangeEcoMode(true, start, end, 40);

            Assert.True(lamp.BrightnessProperty <= 40, $"Brightness should be limited to 40 but was {lamp.BrightnessProperty}");
        }

        [Fact]
        public void TurnOn_SchedulesOffBasedOnDefaultAutoOff()
        {
            var lamp = new EcoLamp(false, "timerTest", ColorType.Red, 50, LampType.LED);

            TimeSpan defaultAutoOff = TimeSpan.FromHours(1);
            TimeSpan ecoAutoOff = TimeSpan.FromMinutes(30);
            lamp.ChangeTimers(defaultAutoOff, ecoAutoOff);

            // Turn on and verify ScheduledOffAt is approximately now + defaultAutoOff
            lamp.TurnOn();

            Assert.True(lamp.ScheduledOffAt.HasValue, "ScheduledOffAt should have a value after TurnOn");

            var expected = DateTime.Now + defaultAutoOff;
            var actual = lamp.ScheduledOffAt.Value;

            var diff = (actual - expected).Duration();
            Assert.True(diff < TimeSpan.FromSeconds(3), $"ScheduledOffAt differs from expected by {diff.TotalSeconds}s");
        }
    }
}