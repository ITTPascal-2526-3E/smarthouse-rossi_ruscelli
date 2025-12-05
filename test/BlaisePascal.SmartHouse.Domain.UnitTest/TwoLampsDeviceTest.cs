using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Lamps;

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
            Assert.Equal(lamp1, twoLampsDevice.Lamp);
            Assert.Equal(lamp2, twoLampsDevice.EcoLamp);
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
    }
}
