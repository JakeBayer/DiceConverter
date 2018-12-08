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
    public class SlidingTileComparer : ITileComparer
    {
        private int _squareSize;
        public SlidingTileComparer(int squareSize)
        {
            _squareSize = squareSize;
        }

        public double GetTileDifference(Tile t1, Tile t2)
        {
            throw new NotImplementedException();
        }

        public double GetTileDifference<TDistanceMetric, TColorMetric>(Tile t1, Tile t2, TDistanceMetric distanceMetric, TColorMetric colorMetric) where TDistanceMetric : IDistanceMetric<int> where TColorMetric : IDistanceMetric<Color>
        {
            double totalDistance = 0;

            for (int x = 0; x <= t1.Width - _squareSize; x++)
            {
                for (int y = 0; y <= t1.Height - _squareSize; y++)
                {
                    totalDistance += colorMetric.Distance(t1.GetAverageColorAt(x, y, _squareSize), t2.GetAverageColorAt(x, y, _squareSize));
                }
            }
            return totalDistance;
        }
    }
}
