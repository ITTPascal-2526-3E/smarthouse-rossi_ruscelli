using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Lamps;
using System.Drawing.Text;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class TwoLampsDeviceTest
    {
        [Fact]
        public void ConstructorAndProperties_ShouldInitializeCorrectly()
        {
            // Arrange
            Lamp lamp1 = new Lamp(true, "temporanea", ColorType.Blue, 10, LampType.Induction);

            EcoLamp lamp2 = new EcoLamp(true, "temporanea2", ColorType.Blue, 10, LampType.Induction);
           
            // Act
            var twoLampsDevice = new TwoLampDevice(lamp1, lamp2);
            // Assert
            Assert.Equal(lamp1, twoLampsDevice.lampProperty);
            Assert.Equal(lamp2, twoLampsDevice.ecolampProperty);
        }
        [Fact]
        public void TurnOffBothLamps_ShouldUpdateTheirStatus()
        {
            // Arrange
            Lamp lamp1 = new Lamp(true, "temporanea2", ColorType.Blue, 10, LampType.Induction);

            EcoLamp lamp2 = new EcoLamp(true, "temporanea2", ColorType.Blue, 10, LampType.Induction);
            // Act
            TwoLampDevice twoLampsDevice = new TwoLampDevice(lamp1, lamp2);
            twoLampsDevice.TurnOffBoth();
            // Assert
            Assert.False(lamp1.IsOnProperty);
            Assert.False(lamp2.IsOnProperty);

        }
        [Fact]
        public void TurnOnBothLamps_ShouldUpdateTheirStatus()
        {
            // Arrange
            Lamp lamp1 = new Lamp(true, "temporanea2", ColorType.Blue, 10, LampType.Induction);

            EcoLamp lamp2 = new EcoLamp(true, "temporanea2", ColorType.Blue, 10, LampType.Induction);
            // Act
            TwoLampDevice twoLampsDevice = new TwoLampDevice(lamp1, lamp2);
            twoLampsDevice.TurnOnBoth();
            // Assert
            Assert.True(lamp1.IsOnProperty);
            Assert.True(lamp2.IsOnProperty);

        }
        [Fact]
        public void ChangeIntesityOfBoth_fromEcoLampBrightnessToLamp_ShouldUpdateTheLampBrightness()
        {
            // Arrange
            Lamp lamp1 = new Lamp(true, "temporanea2", ColorType.Blue, 10, LampType.Induction);

            EcoLamp lamp2 = new EcoLamp(true, "temporanea2", ColorType.Blue, 20, LampType.Induction);

            TwoLampDevice test = new TwoLampDevice(lamp1, lamp2);
            // Act
            test.SetSameBrightness(false);
            // Assert
            Assert.Equal(20, lamp1.BrightnessProperty);
            Assert.Equal(20, lamp2.BrightnessProperty);


        }
        [Fact]
        public void ChangeIntesityOfBoth_fromLampToEcoLamp_ShouldUpdateTheEcoLampBrightness()
        {
            // Arrange
            Lamp lamp1 = new Lamp(true, "temporanea2", ColorType.Blue, 20, LampType.Induction);

            EcoLamp lamp2 = new EcoLamp(true, "temporanea2", ColorType.Blue, 10, LampType.Induction);

            TwoLampDevice test = new TwoLampDevice(lamp1, lamp2);
            // Act
            test.SetSameBrightness(true);
            // Assert
            Assert.Equal(20, lamp1.BrightnessProperty);
            Assert.Equal(20, lamp2.BrightnessProperty);


        }
        [Fact]
        public void ChangeIntesityOfBoth_fromLampToEcoLamp_ButEcoLampBrightnessCannotBeSetBecauseOfEcoMode()
        {
            // Arrange
            Lamp lamp1 = new Lamp(true, "temporanea2", ColorType.Blue, 20, LampType.Induction);

            EcoLamp lamp2 = new EcoLamp(true, "temporanea2", ColorType.Blue, 10, LampType.Induction);

            // put eco lamp in eco mode with max brightness 10 (and mirror that to the property used by TwoLampDevice)
            DateTime now = DateTime.Now;
            TimeOnly start = TimeOnly.FromDateTime(now.AddHours(-1));
            TimeOnly end = TimeOnly.FromDateTime(now.AddHours(1));
            lamp2.ChangeEcoMode(true, start, end, 10);
            

            TwoLampDevice test = new TwoLampDevice(lamp1, lamp2);
            // Act
            
            // Assert
            // lamp1 should remain at 20, eco lamp should remain at its previous capped value 10
            Assert.Equal(20, lamp1.BrightnessProperty);
            Assert.Equal(10, lamp2.BrightnessProperty);

        }
    }
}
