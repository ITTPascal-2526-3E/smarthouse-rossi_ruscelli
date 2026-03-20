using BlaisePascal.SmartHouse.Domain.Abstractions;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;
using BlaisePascal.SmartHouse.Domain.Lightning;
using System;
using Xunit;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class EcoLampTest
    {
       
        private EcoLamp CreateLamp(bool isOn = false, int brightness = 50)
        {
            return new EcoLamp(
            isOn,
            new NameDevice("test"),
            ColorType.Red,
            new Brightness(brightness),
            LampType.LED
            );
        }



    [Fact]
        public void PowerConsumption_WhenOff_ReturnsZero()
        {
            var lamp = CreateLamp(false);

            Assert.Equal(0f, lamp.PowerConsumption);
        }

        [Fact]
        public void PowerConsumption_WhenOn_IsCalculatedCorrectly()
        {
            var lamp = CreateLamp(true, 50);

            float expected =
                AbstractLamp.GetMaxConsumption(LampType.LED).Consumption *
                (50f / 100f) *
                AbstractLamp.GetAlpha(LampType.LED);

            float actual = lamp.PowerConsumption;

            Assert.True(Math.Abs(expected - actual) < 0.0001f);
        }

      

        [Fact]
        public void TurnOnEco_WhenAlreadyOn_ShouldStillSetSchedule()
        {
            var lamp = CreateLamp(true);

            lamp.TurnOnEco();

            Assert.True(lamp.IsOnProperty);
            Assert.NotNull(lamp.ScheduledOffAt);
        }

       

       

        [Fact]
        public void ChangeEcoMode_Disabled_ShouldNotLimitBrightness()
        {
            var lamp = CreateLamp(true, 80);

            var now = DateTime.Now;
            var start = TimeOnly.FromDateTime(now.AddHours(-1));
            var end = TimeOnly.FromDateTime(now.AddHours(1));

            lamp.ChangeEcoMode(false, start, end, 40);

            Assert.Equal(80, lamp.BrightnessProperty.Value);
        }



        [Fact]
        public void ChangeTimers_ShouldUpdateScheduledOff()
        {
            var lamp = CreateLamp(true);

            lamp.ChangeTimers(TimeSpan.FromHours(2), TimeSpan.FromHours(1));

            Assert.NotNull(lamp.ScheduledOffAt);
        }

        [Fact]
        public void ChangeTimers_WhenLampOff_ShouldNotCrash()
        {
            var lamp = CreateLamp(false);

            lamp.ChangeTimers(TimeSpan.FromHours(2), TimeSpan.FromHours(1));

            Assert.False(lamp.IsOnProperty);
        }



        [Fact]
        public void IsInEco_ShouldReturnTrue_WhenInsideWindow()
        {
            var lamp = CreateLamp();

            var now = DateTime.Now;
            var start = TimeOnly.FromDateTime(now.AddMinutes(-10));
            var end = TimeOnly.FromDateTime(now.AddMinutes(10));

            lamp.ChangeEcoMode(true, start, end, 50);

            Assert.True(lamp.IsInEco(now));
        }

        [Fact]
        public void IsInEco_ShouldReturnFalse_WhenOutsideWindow()
        {
            var lamp = CreateLamp();

            var now = DateTime.Now;
            var start = TimeOnly.FromDateTime(now.AddHours(1));
            var end = TimeOnly.FromDateTime(now.AddHours(2));

            lamp.ChangeEcoMode(true, start, end, 50);

            Assert.False(lamp.IsInEco(now));
        }

        [Fact]
        public void IsInEco_ShouldHandleMidnightCrossing()
        {
            var lamp = CreateLamp();

            var start = new TimeOnly(22, 0);
            var end = new TimeOnly(6, 0);

            lamp.ChangeEcoMode(true, start, end, 50);

            var testTime = DateTime.Today.AddHours(23);

            Assert.True(lamp.IsInEco(testTime));
        }



        [Fact]
        public void NextOccurrence_ShouldReturnFutureDate()
        {
            var lamp = CreateLamp();

            var now = DateTime.Now;
            var time = TimeOnly.FromDateTime(now.AddHours(-1));

            var result = lamp.NextOccurrence(now, time);

            Assert.True(result > now);
        }



        [Fact]
        public void ComputeFinalOffInstant_NoEco_ReturnsDefault()
        {
            var lamp = CreateLamp(true);

            lamp.ChangeTimers(TimeSpan.FromHours(2), TimeSpan.FromHours(1));

            var now = DateTime.Now;
            var result = lamp.ComputeFinalOffInstant(now);

            Assert.Equal(now + TimeSpan.FromHours(2), result);
        }

        [Fact]
        public void ComputeFinalOffInstant_WhenInEco_UsesEcoTimer()
        {
            var lamp = CreateLamp(true);

            var now = DateTime.Now;
            var start = TimeOnly.FromDateTime(now.AddHours(-1));
            var end = TimeOnly.FromDateTime(now.AddHours(1));

            lamp.ChangeEcoMode(true, start, end, 50);
            lamp.ChangeTimers(TimeSpan.FromHours(5), TimeSpan.FromHours(1));

            var result = lamp.ComputeFinalOffInstant(now);

            Assert.Equal(now + TimeSpan.FromHours(1), result);
        }

        [Fact]
        public void ComputeFinalOffInstant_WhenEcoStartsLater_ChoosesEarlier()
        {
            var lamp = CreateLamp(true);

            var now = DateTime.Now;

            var start = TimeOnly.FromDateTime(now.AddMinutes(30));
            var end = TimeOnly.FromDateTime(now.AddHours(2));

            lamp.ChangeEcoMode(true, start, end, 50);
            lamp.ChangeTimers(TimeSpan.FromHours(3), TimeSpan.FromHours(1));

            var result = lamp.ComputeFinalOffInstant(now);

            Assert.True(result <= now + TimeSpan.FromHours(3));
        }



        [Fact]
        public void StaticHelpers_ReturnExpectedValues()
        {
            var max = AbstractLamp.GetMaxConsumption(LampType.LED);
            var alpha = AbstractLamp.GetAlpha(LampType.LED);

            Assert.Equal(25, max.Consumption);
            Assert.Equal(0.2f, alpha);
        }

    

 

     
    }
}
