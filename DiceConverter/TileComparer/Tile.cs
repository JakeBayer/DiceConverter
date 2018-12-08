using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceConverter.TileComparer
{
    public class Tile
    {
        public int Width  => Image.Width;
        public int Height => Image.Height;

        public Bitmap Image { get; }

        public Color GetPixel(int i, int j) => Image.GetPixel(i, j);

        private readonly Dictionary<int, Dictionary<int, Color>> _avgColorCache = new Dictionary<int, Dictionary<int, Color>>();

        public Tile(Bitmap img)
        {
            Image = img;
        }

        public Color GetAverageColorAt(int x, int y, int squareSize)
        {
            var squareNo = y * Width + x;
            if (!_avgColorCache.ContainsKey(squareNo))
            {
                _avgColorCache.Add(squareNo, new Dictionary<int, Color>());
            }
            if (!_avgColorCache[squareNo].ContainsKey(squareSize))
            {
                _avgColorCache[squareNo].Add(squareSize, CalculateAverageColorAt(x, y, squareSize));
            }
            return _avgColorCache[squareNo][squareSize];
        }

        private Color CalculateAverageColorAt(int x, int y, int squareSize)
        {
            double r = 0, g = 0, b = 0;
            for (int i = 0; i < squareSize; i++)
            {
                for (int j = 0; j < squareSize; j++)
                {
                    var pixel = Image.GetPixel(x + i, y + j);
                    r += pixel.R * pixel.R;
                    g += pixel.G * pixel.G;
                    b += pixel.B * pixel.B;
                }
            }
            var num = squareSize * squareSize;
            return Color.FromArgb((int) Math.Sqrt(r / num), (int) Math.Sqrt(g / num), (int) Math.Sqrt(b / num));
        }
    }
}
