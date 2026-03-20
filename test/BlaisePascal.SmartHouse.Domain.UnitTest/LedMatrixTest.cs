using System;
using Xunit;
using BlaisePascal.SmartHouse.Domain.Lightning;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.UnitTest
{
    public class LedMatrixTest
    {
        private LedMatrix CreateMatrix(bool isOn = true, int brightness = 100)
        {
            return new LedMatrix(
                new Width(2),
                new Height(2),
                ColorType.CoolWhite,
                isOn,
                new NameDevice("test"),
                new Brightness(brightness),
                LampType.LED
            );
        }

        [Fact]
        public void ConstructorAndProperties_ShouldInitializeCorrectly()
        {
            var ledMatrix = CreateMatrix();

            var matrix = ledMatrix.MatrixProperty;

            Assert.Equal(2, ledMatrix.HeightProperty);
            Assert.Equal(2, ledMatrix.WidthProperty);
            Assert.NotNull(matrix);

            for (int i = 0; i < ledMatrix.HeightProperty; i++)
            {
                for (int j = 0; j < ledMatrix.WidthProperty; j++)
                {
                    Assert.True(matrix[i, j].IsOnProperty);
                    Assert.Equal("test", matrix[i, j].NameProperty);
                    Assert.Equal(ColorType.CoolWhite, matrix[i, j].ColorProperty);
                    Assert.Equal(100, matrix[i, j].BrightnessProperty.Value);
                    Assert.Equal(LampType.LED, matrix[i, j].LampTypeProperty);
                }
            }
        }

        [Fact]
        public void TurnOnAllLamps_ShouldUpdateTheirState()
        {
            var ledMatrix = CreateMatrix(isOn: false);

            ledMatrix.TurnOnAll();

            var matrix = ledMatrix.MatrixProperty;

            for (int i = 0; i < ledMatrix.HeightProperty; i++)
            {
                for (int j = 0; j < ledMatrix.WidthProperty; j++)
                {
                    Assert.True(matrix[i, j].IsOnProperty);
                }
            }
        }

        [Fact]
        public void TurnOffAllLamps_ShouldUpdateTheirState()
        {
            var ledMatrix = CreateMatrix(isOn: true);

            ledMatrix.TurnOffAll();

            var matrix = ledMatrix.MatrixProperty;

            for (int i = 0; i < ledMatrix.HeightProperty; i++)
            {
                for (int j = 0; j < ledMatrix.WidthProperty; j++)
                {
                    Assert.False(matrix[i, j].IsOnProperty);
                }
            }
        }

        [Fact]
        public void SetSameBrightness_ShouldUpdateTheirBrightness()
        {
            var ledMatrix = CreateMatrix(brightness: 100);

            ledMatrix.SetSameBrightness(new Brightness(10));

            var matrix = ledMatrix.MatrixProperty;

            for (int i = 0; i < ledMatrix.HeightProperty; i++)
            {
                for (int j = 0; j < ledMatrix.WidthProperty; j++)
                {
                    Assert.Equal(10, matrix[i, j].BrightnessProperty.Value);
                }
            }
        }

        [Fact]
        public void SetSameBrightness_ClampedValue_ShouldUpdateTheirBrightness()
        {
            var ledMatrix = CreateMatrix(brightness: 100);

            ledMatrix.SetSameBrightness(new Brightness(-10));

            var matrix = ledMatrix.MatrixProperty;

            for (int i = 0; i < ledMatrix.HeightProperty; i++)
            {
                for (int j = 0; j < ledMatrix.WidthProperty; j++)
                {
                    // rimane invariato (100)
                    Assert.Equal(0, matrix[i, j].BrightnessProperty.Value);
                }
            }
        }
        [Fact]
        public void GetLampsInRowAndColumn_ShouldReturnSameInstances_AsMatrix()
        {
            var ledMatrix = new LedMatrix(
                new Width(2),
                new Height(2),
                ColorType.CoolWhite,
                true,
                new NameDevice("test"),
                new Brightness(100),
                LampType.LED
            );

            var matrix = ledMatrix.MatrixProperty;

            var row = ledMatrix.GetLampsInRow(1);
            var column = ledMatrix.GetLampsInColumn(1);

            // stessa istanza nella riga
            Assert.Same(matrix[1, 0], row[0]);
            Assert.Same(matrix[1, 1], row[1]);

            // stessa istanza nella colonna
            Assert.Same(matrix[0, 1], column[0]);
            Assert.Same(matrix[1, 1], column[1]);
        }
    }
}