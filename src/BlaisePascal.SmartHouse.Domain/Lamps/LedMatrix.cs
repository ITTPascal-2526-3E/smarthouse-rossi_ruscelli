using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class LedMatrix
    {
        private Guid Id; // Unique identifier for the LED matrix
        private int Width; // Width (number of columns) of the LED matrix
        private int Height; // Height (number of rows) of the LED matrix
        private Lamp[,] matrix; 

        public LedMatrix(int width, int height, ColorType color)
        {
            Id = Guid.NewGuid();
            Width = width;
            Height = height;
            // initialize matrix with default Lamps (turned off, 0 brightness) so methods can be used safely
            matrix = new Lamp[Height, Width];
            for (int r = 0; r < Height; r++)
            {
                for (int c = 0; c < Width; c++)
                {
                    matrix[r, c] = new Lamp(false, string.Empty, color, 0, LampType.LED);
                }
            }
        }

        public int WidthProperty
        {
            get { return Width; }
            set { Width = value; }
        }
         
        public int HeightProperty
        {
            get { return Height; }
            set { Height = value; }
        }

        public void InitializeMatrix(bool isOn, string name, ColorType color, int brightness, LampType lampType)
        {
            matrix = new Lamp[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    matrix[i, j] = new Lamp(isOn, name, color, brightness, lampType);
                }
            }
        }
        public void TurnOnAllLamps()
        {
          
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    matrix[i, j].TurnOn();
                }
            }
        }
        public void TurnOffAllLamps()
        {
           
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    matrix[i, j].TurnOff();
                }
            }
        }

        public void SetIntensityAll(int intensity)
        {
          
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    matrix[i, j].ChangeBrightness(intensity);
                }
            }
        }

        public Lamp GetLamp(int row, int column)
        {
            return matrix[row, column];
        }
        public Lamp[] GetLampsInRow(int row)
        {
            Lamp[] lampsInRow = new Lamp[Width];
            for (int j = 0; j < Width; j++)
            {
                lampsInRow[j] = matrix[row, j];
            }
            return lampsInRow;
        }
        public Lamp[] GetLampsInColumn(int column)
        {
            Lamp[] lampsInColumn = new Lamp[Height];
            for (int i = 0; i < Height; i++)
            {
                lampsInColumn[i] = matrix[i, column];
            }
            return lampsInColumn;
        }


    }
}





