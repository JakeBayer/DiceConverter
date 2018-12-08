using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceConverter.Distance
{
    public interface IDistanceMetric<in T>
    {
        double Distance(T a, T b);
    }
}
