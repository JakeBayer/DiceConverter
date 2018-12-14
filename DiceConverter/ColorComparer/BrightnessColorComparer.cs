using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceConverter.Distance;

namespace DiceConverter.ColorComparer
{
    public class BrightnessColorComparer : IDistanceMetric<Color>
    {
        public double Distance(Color a, Color b)
        {
            return Math.Abs(a.GetBrightness() - b.GetBrightness());
        }
    }
}
