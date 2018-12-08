using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceConverter.Distance;

namespace DiceConverter.ColorComparer
{
    public class EuclideanColorComparer : IDistanceMetric<Color>
    {
        public double Distance(Color a, Color b)
        {
            
            var r = (a.R + b.R) / 2;
            var dR = (a.R - b.R);
            var dG = (a.G - b.G);
            var dB = (a.B - b.B);
            return Math.Sqrt((2 * dR * dR) + (4 * dG * dG) + (3 * dB * dB) + (r * (dR * dR - dB * dB)) / 256);
        }
    }
}
