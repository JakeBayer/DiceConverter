using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceConverter
{
    public class Tile
    {
        public Color[,] Pixels { get; }

        public Color this[int i, int j] => Pixels[i, j];

        public Tile(ICollection<Color> colors)
        {
            if (!IsSquare(colors.Count, out var sqrt))
            {
                throw new InvalidOperationException("Tile Must be square, ya dingus");
            }
            Pixels = FromCollection(colors, sqrt);
        }

        private bool IsSquare(long number, out long sqrt)
        {
            sqrt = (long)Math.Sqrt(Convert.ToDouble(number));
            return number == sqrt * sqrt;
        }

        private Color[,] FromCollection(ICollection<Color> colors, long sideLength)
        {
            var colorArray = new Color[sideLength,sideLength];
            long x = 0, y = 0;
            foreach (var color in colors)
            {
                colorArray[x, y] = color;
                x++;
                if (x >= sideLength)
                {
                    x = 0;
                    y++;
                }
            }

            return colorArray;
        }
    }
}
