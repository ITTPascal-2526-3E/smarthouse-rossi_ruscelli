using System;
using BlaisePascal.SmartHouse.Domain.Lamps;
using Xunit;

namespace BlaisePascal.SmartHouse.Domain.UnitTests
{
    public class LampTest
    {
        [Fact]
        public void ConstructorAndProperties_ShouldInitializeCorrectly()
        {
            var lamp = new Lamp(true, "TestLamp", ColorType.Red, 80, LampType.CFL);

            Assert.True(lamp.IsOnProperty);
            Assert.Equal("TestLamp", lamp.NameProperty);
            Assert.Equal(ColorType.Red, lamp.ColorProperty);
            Assert.Equal(80, lamp.BrightnessProperty);
            Assert.Equal(LampType.CFL, lamp.LampTypeProperty);
        }

        [Fact]
        public void TurnOn_TurnOff_And_PowerConsumption_ShouldBehaveAsExpected()
        {
            var lamp = new Lamp(false, "L", ColorType.CoolWhite, 50, LampType.LED);

            // lamp is off consumption is zero
            Assert.False(lamp.IsOnProperty);
            Assert.Equal(0f, lamp.PowerConsumption);

            // turn on consumption calculated
            lamp.TurnOn();
            Assert.True(lamp.IsOnProperty);

            float expected = Lamp.GetMaxConsumption(LampType.LED) * (50f / 100f) * Lamp.GetAlpha(LampType.LED);
            Assert.Equal(expected, lamp.PowerConsumption, 3);

            // turn off consumption zero again
            lamp.TurnOff();
            Assert.False(lamp.IsOnProperty);
            Assert.Equal(0f, lamp.PowerConsumption);
        }

        [Fact]
        public void ChangeBrightness_ShouldAcceptValidAndRejectInvalidValues()
        {
            var lamp = new Lamp(true, "B", ColorType.Daylight, 20, LampType.Incandescent);

            lamp.ChangeBrightness(70);
            Assert.Equal(70, lamp.BrightnessProperty);

            // invalid change should be ignored
            lamp.ChangeBrightness(200);
            Assert.Equal(70, lamp.BrightnessProperty);

            // direct property setter also ignores invalid values
            lamp.BrightnessProperty = -10;
            Assert.Equal(70, lamp.BrightnessProperty);
        }

        [Fact]
        public void ChangeColor_ShouldUpdateColor()
        {
            var lamp = new Lamp(true, "C", ColorType.WarmWhite, 30, LampType.Halogen);

            lamp.ChangeColor(ColorType.Purple);
            Assert.Equal(ColorType.Purple, lamp.ColorProperty);
        }

        [Fact]
        public void StaticMethods_GetMaxConsumptionAndGetAlpha_ReturnExpectedValues()
        {
            // verify a couple of known mappings
            Assert.Equal(25, Lamp.GetMaxConsumption(LampType.LED));
            Assert.Equal(0.2f, Lamp.GetAlpha(LampType.LED), 3);

            Assert.Equal(300, Lamp.GetMaxConsumption(LampType.Incandescent));
            Assert.Equal(1.0f, Lamp.GetAlpha(LampType.Incandescent), 3);
        }

        [Fact]
        public void Properties_Setters_ShouldUpdateValues()
        {
            var lamp = new Lamp(false, "Orig", ColorType.WarmWhite, 10, LampType.VintageLED);

            lamp.NameProperty = "NewName";
            lamp.ColorProperty = ColorType.CoolWhite;
            lamp.LampTypeProperty = LampType.CFL;

            Assert.Equal("NewName", lamp.NameProperty);
            Assert.Equal(ColorType.CoolWhite, lamp.ColorProperty);
            Assert.Equal(LampType.CFL, lamp.LampTypeProperty);
        }

        [Fact]
        public void PowerConsumption_ReflectsBrightnessChange_WhenOn()
        {
            var lamp = new Lamp(true, "P", ColorType.Daylight, 25, LampType.Halogen);

            // initial consumption at 25%
            float expected25 = Lamp.GetMaxConsumption(LampType.Halogen) * (25f / 100f) * Lamp.GetAlpha(LampType.Halogen);
            Assert.Equal(expected25, lamp.PowerConsumption, 3);

            // increase brightness to 75%
            lamp.ChangeBrightness(75);
            float expected75 = Lamp.GetMaxConsumption(LampType.Halogen) * (75f / 100f) * Lamp.GetAlpha(LampType.Halogen);
            Assert.Equal(expected75, lamp.PowerConsumption, 3);
        }

        [Fact]
        public void TurnOn_TurnOff_Behavior()
        {
            var lamp = new Lamp(true, "I", ColorType.Red, 60, LampType.LED);

            // turning on when already on should keep it on
            lamp.TurnOn();
            Assert.True(lamp.IsOnProperty);

            // turning off works
            lamp.TurnOff();
            Assert.False(lamp.IsOnProperty);

            // turning off again remains off
            lamp.TurnOff();
            Assert.False(lamp.IsOnProperty);
        }
    }
}
