using BlaisePascal.SmartHouse.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Lamps.LampsInterfaces;
using BlaisePascal.SmartHouse.Domain.Abstractions.VO;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public sealed class LedMatrix : AbstractDevice, IMultipleDevices, IMultipleDevicesDimmable, ILedMatrix
    {
        
        private Width Width; // Width (number of columns) of the LED matrix
        private Height Height; // Height (number of rows) of the LED matrix
        private Lamp[,] matrix; 

        public LedMatrix(Width width, Height height, ColorType color, bool isOn, NameDevice name, Brightness brightness, LampType lampType) : base(name)
        {
           
            Width = width;
            Height = height;
            // initialize matrix with default Lamps (turned off, 0 brightness) so methods can be used safely
            matrix = new Lamp[((int)(Height.Value)), ((int)(Width.Value))];
            for (int r = 0; r < Height.Value; r++)
            {
                for (int c = 0; c < Width.Value; c++)
                {
                    matrix[r, c] = new Lamp(isOn, name, color, brightness, lampType);
                }
            }
        }
        public int WidthProperty { get; private set; }  //todo
        public int HeightProperty { get; private set; }  //todo
        public Lamp[,] MatrixProperty { get; private set; } 

        public void TurnOnAll()
        {

            for (int i = 0; i < Height.Value; i++)
            {
                for (int j = 0; j < Width.Value; j++)
                {
                    matrix[i, j].TurnOn();
                }
            }
        }
        public void TurnOffAll()
        {

            for (int i = 0; i < Height.Value; i++)
            {
                for (int j = 0; j < Width.Value; j++)
                {
                    matrix[i, j].TurnOff();
                }
            }
        }

        public void SetSameBrightness(Brightness brightness)
        {
            if(brightness.Value < 0 || brightness.Value > 100)
            {
                return;
            }
            for (int i = 0; i < Height.Value; i++)
            {
                for (int j = 0; j < Width.Value; j++)
                {
                    matrix[i, j].ChangeBrightness(brightness);
                }
            }
        }

        public Lamp GetLamp(int row, int column)
        {
            return matrix[row, column];
        }
        public Lamp[] GetLampsInRow(int row)
        {
            Lamp[] lampsInRow = new Lamp[((int)Width.Value)];
            for (int j = 0; j < Width.Value; j++)
            {
                lampsInRow[j] = matrix[row, j];
            }
            return lampsInRow;
        }
        public Lamp[] GetLampsInColumn(int column)
        {
            Lamp[] lampsInColumn = new Lamp[((int)Height.Value)];
            for (int i = 0; i < Height.Value; i++)
            {
                lampsInColumn[i] = matrix[i, column];
            }
            return lampsInColumn;
        }


    }
}





