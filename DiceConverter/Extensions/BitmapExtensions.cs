using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceConverter.Extensions
{
    public static class BitmapFactory
    {
        public static Bitmap FromColorArray(Color[,] colorArray)
        {
            var width = colorArray.GetLength(0);
            var height = colorArray.GetLength(1);

            var bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color c = colorArray[x, y];
                    bitmap.SetPixel(x, y, c);
                }
            }

            return bitmap;
        }
    }
}
