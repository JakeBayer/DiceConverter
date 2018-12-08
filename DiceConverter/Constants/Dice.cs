using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceConverter.Constants
{
    public static class DiceTiles
    {
        private static readonly Color B = Color.Black;
        private static readonly Color W = Color.White;

        private static readonly Color[] One = new Color[]
        {
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, B, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
        };

        private static readonly Color[] Two = new Color[]
        {
            W, W, W, W, W, W, W,
            W, W, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, W, W,
            W, W, W, W, W, W, W,
        };

        private static readonly Color[] Two_R = new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, B, W,
            W, W, W, W, W, W, W,
        };
        private static readonly Color[] Three = new Color[]
        {
            W, W, W, W, W, W, W,
            W, W, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, B, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, W, W,
            W, W, W, W, W, W, W,
        };
        private static readonly Color[] Three_R = new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, B, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, B, W,
            W, W, W, W, W, W, W,
        };
        private static readonly Color[] Four = new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
        };
        private static readonly Color[] Five = new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, B, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
        };
        private static readonly Color[] Six = new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
        };
        private static readonly Color[] Six_R = new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, B, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, B, W, B, W,
            W, W, W, W, W, W, W,
        };

        public static Color[,] ToArray(Color[] arr, int size)
        {
            var  colorArray = new Color[size, size];
            long x          = 0, y = 0;
            foreach (var color in arr)
            {
                colorArray[x, y] = color;
                x++;
                if (x >= size)
                {
                    x = 0;
                    y++;
                }
            }

            return colorArray;
        }

        public static IEnumerable<Color[,]> Tiles { get; } = new Color[][]
        {
            One, Two, Two_R, Three, Three_R, Four, Five, Six, Six_R
        }.Select(a => ToArray(a, 7));
    }
}
