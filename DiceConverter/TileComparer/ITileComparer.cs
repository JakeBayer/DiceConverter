using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceConverter.Distance;
using DiceConverter.TileComparer;

namespace DiceConverter.TileComparer
{
    public interface ITileComparer
    {
        double GetTileDifference(Tile t1, Tile t2);

        double GetTileDifference<TDistanceMetric, TColorMetric>(Tile t1, Tile t2, TDistanceMetric distanceMetric, TColorMetric colorMetric)
            where TDistanceMetric : IDistanceMetric<int>
            where TColorMetric : IDistanceMetric<Color>;
    }
}
