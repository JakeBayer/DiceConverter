using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceConverter.Distance
{
    public class EuclideanMetric : IDistanceMetric<int>
    {
        public double Distance(int a, int b)
        {
            return Math.Sqrt(a * a + b * b);
        }
    }
}
