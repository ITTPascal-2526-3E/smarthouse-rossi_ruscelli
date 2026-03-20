using System;
using System.Security.Cryptography.X509Certificates;
using BlaisePascal.SmartHouse.Domain.Lightning;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Abstractions;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class LampTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var lamp = new Lamp(true, new NameDevice("test"), ColorType.Orange, new Brightness(100), LampType.LED);
            Assert.True(lamp.IsOnProperty);
            Assert.Equal("test", lamp.NameProperty);
            Assert.Equal(ColorType.Orange, lamp.ColorProperty);
            Assert.Equal(100, lamp.BrightnessProperty.Value);
            Assert.Equal(LampType.LED, lamp.LampTypeProperty);
        }


        [Fact]
            public void ChangeLampType_WhenOff_ConsumptionRemainsZero()
            {
                var lamp = new Lamp(false, new NameDevice("Lamp1"), ColorType.CoolWhite, new Brightness(100), LampType.LED);

                lamp.ChangeLampType(LampType.Halogen);

                Assert.Equal(0, lamp.PowerConsumption);
            }


            [Fact]
            public void ChangeColor_DoesNotAffectPowerConsumption()
            {
                var lamp = new Lamp(true, new NameDevice("Lamp1"), ColorType.CoolWhite, new Brightness(100), LampType.LED);

                float before = lamp.PowerConsumption;

                lamp.ChangeColor(ColorType.Red);

                Assert.Equal(before, lamp.PowerConsumption, 3);
            }


            [Fact]
            public void ChangeBrightness_NegativeValue_BecomesZero()
            {
                var lamp = new Lamp(true, new NameDevice("Lamp1"), ColorType.CoolWhite, new Brightness(50), LampType.LED);
                // negative values are clamped by Brightness

                lamp.ChangeBrightness(new Brightness(-10));

               Assert.Equal(0, lamp.BrightnessProperty.Value);
               Assert.Equal(0, lamp.PowerConsumption);
            }
           

            [Fact]
            public void ChangeBrightness_Zero_WhenOn_ConsumptionBecomesZero()
            {
                var lamp = new Lamp(true, new NameDevice("Lamp1"), ColorType.CoolWhite, new Brightness(50), LampType.LED);

                lamp.ChangeBrightness(new Brightness(0));

                Assert.Equal(0, lamp.PowerConsumption);
            }
        

        [Fact]
        public void TurnOn_shouldBeTurnedOn()
        {
            var lamp = new Lamp(false, new NameDevice("Lamp1"), ColorType.CoolWhite, new Brightness(100), LampType.LED);
            lamp.TurnOn();
            Assert.True(lamp.IsOnProperty);
        }
        [Fact]
        public void TurnOff_shouldBeTurnedOff()
        {
            var lamp = new Lamp(true, new NameDevice("Lamp1"), ColorType.CoolWhite, new Brightness(100), LampType.LED);
            lamp.TurnOff();
            Assert.False(lamp.IsOnProperty);
            
        }
        [Fact]
        public void PowerConsumption_WhenOff_IsZero()
        {
            var lamp = new Lamp(false, new NameDevice("Lamp1"), ColorType.CoolWhite, new Brightness(100), LampType.LED);

            Assert.Equal(0, lamp.PowerConsumption);
        }
      
        [Fact]
        public void PowerConsumption_WhenOnAndFullBrightness_IsCorrect()
        {
            var lamp = new Lamp(true, new NameDevice("Lamp1"), ColorType.CoolWhite, new Brightness(100), LampType.LED);

            Assert.Equal(5f, lamp.PowerConsumption, 3);
        }

        [Fact]
        public void TurnOn_EnablesConsumption()
        {
            var lamp = new Lamp(false, new NameDevice("Lamp1"), ColorType.CoolWhite, new Brightness(50), LampType.LED);

            lamp.TurnOn();

            Assert.True(lamp.PowerConsumption > 0);
        }
        [Fact]
        public void ChangeBrightness_WhenOn_UpdatesBrightness()
        {
            var lamp = new Lamp(true,new NameDevice("Lamp1"),ColorType.CoolWhite,new Brightness(50),LampType.LED);

            lamp.ChangeBrightness(new Brightness(80));

            Assert.Equal(80, lamp.BrightnessProperty.Value);
        }
    }
}