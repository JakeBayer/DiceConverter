using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceConverter.Distance;

namespace DiceConverter.TileComparer
{
    public interface ITileComparer
    {
        double GetTileDifference(Bitmap t1, Bitmap t2);

        double GetTileDifference<TDistanceMetric, TColorMetric>(Bitmap t1, Bitmap t2, TDistanceMetric distanceMetric, TColorMetric colorMetric)
            where TDistanceMetric : IDistanceMetric<int>
            where TColorMetric : IDistanceMetric<Color>;
    }
}
