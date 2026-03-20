using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Lightning;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class LedMatrixTest
    {
        [Fact]
        public void ConstructorAndProperties_ShouldInitializeCorrectly()
        {
            var ledMatrix = new LedMatrix(new Width(2), new Height(2), ColorType.CoolWhite, true, new NameDevice("test"), new Brightness(100), LampType.LED);
            Lamp[,] ledmatrixtest = ledMatrix.MatrixProperty;
            Assert.Equal(2, ledMatrix.HeightProperty);
            Assert.Equal(2, ledMatrix.WidthProperty);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.True(ledmatrixtest[i, j].IsOnProperty);
                    Assert.Equal("test", ledmatrixtest[i, j].NameProperty);
                    Assert.Equal(ColorType.CoolWhite, ledmatrixtest[i, j].ColorProperty);
                    Assert.Equal(100, ledmatrixtest[i, j].BrightnessProperty.Value);
                    Assert.Equal(LampType.LED, ledmatrixtest[i, j].LampTypeProperty);
                }
            }
        }

        [Fact]
        public void TurnOnAllLamps_ShouldUpdateTheirState()
        {
            var ledMatrix = new LedMatrix(new Width(2), new Height(2), ColorType.CoolWhite, false, new NameDevice("test"), new Brightness(100), LampType.LED);
            ledMatrix.TurnOnAll();
            Lamp[,] ledmatrixtest = ledMatrix.MatrixProperty;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.True(ledmatrixtest[i, j].IsOnProperty);
                }
            }
        }

        [Fact]
        public void TurnOffAllLamps_ShouldUpdateTheirState()
        {
            var ledMatrix = new LedMatrix(new Width(2), new Height(2), ColorType.CoolWhite, true, new NameDevice("test"), new Brightness(100), LampType.LED);
            ledMatrix.TurnOffAll();
            Lamp[,] ledmatrixtest = ledMatrix.MatrixProperty;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.False(ledmatrixtest[i, j].IsOnProperty);
                }
            }
        }

        [Fact]
        public void SetIntensityAll_ShouldUpdateTheirBrightness()
        {
            var ledMatrix = new LedMatrix(new Width(2), new Height(2), ColorType.CoolWhite, true, new NameDevice("test"), new Brightness(100), LampType.LED);
            ledMatrix.SetSameBrightness(new Brightness(10));
            Lamp[,] ledmatrixtest = ledMatrix.MatrixProperty;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.Equal(10, ledmatrixtest[i, j].BrightnessProperty.Value);
                }
            }
        }

        [Fact]
        public void SetIntensityAll_InvalidValue_ShouldNotUpdateTheirBrightness()
        {
            var ledMatrix = new LedMatrix(new Width(2), new Height(2), ColorType.CoolWhite, true, new NameDevice("test"), new Brightness(100), LampType.LED);
            ledMatrix.SetSameBrightness(new Brightness(-10));
            Lamp[,] ledmatrixtest = ledMatrix.MatrixProperty;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.Equal(0, ledmatrixtest[i, j].BrightnessProperty.Value);
                }
            }
        }
    }
}