using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceConverter.ColorComparer;
using DiceConverter.Distance;

namespace DiceConverter.TileComparer
{
    public class FuzzyTileComparer : ITileComparer
    {
        private int _maxDistance;
        private static readonly EuclideanMetric _euclidean = new EuclideanMetric();

        public FuzzyTileComparer(int maxDistance)
        {
            _maxDistance = maxDistance;
        }

        public double GetTileDifference(Tile t1, Tile t2)
        {
            return GetTileDifference(t1, t2, _euclidean, new EuclideanColorComparer());
        }

        public double GetTileDifference<TDistanceMetric, TColorMetric>(Tile t1, Tile t2, TDistanceMetric distanceMetric, TColorMetric colorMetric)
            where TDistanceMetric : IDistanceMetric<int>
            where TColorMetric : IDistanceMetric<Color>
        {
            double totalDistance = 0;

            for (int x = 0; x <= t1.Width; x++)
            {
                for (int y = 0; y <= t1.Height; y++)
                {
                    totalDistance += GetDifferenceAtPixel(x, y, t1, t2, _maxDistance, distanceMetric, colorMetric);
                }
            }
            return totalDistance;
        }

        private double GetDifferenceAtPixel(int x, int y, Tile t1, Tile t2, int maxDistance, IDistanceMetric<int> distanceMetric, IDistanceMetric<Color> colorMetric)
        {
            var max_x = t1.Width;
            var max_y = t1.Height;

            bool IsValid(int pos_x, int pos_y)
            {
                return pos_x >= 0 && pos_x < max_x &&
                       pos_y >= 0 && pos_y < max_y;
            }

            double totalDistance = 0;

            for (int i = -maxDistance; i <= maxDistance; i++)
            {
                for (int j = -maxDistance; j <= maxDistance; j++)
                {
                    if (distanceMetric.Distance(i - x, j - y) <= maxDistance && IsValid(i, j))
                    {
                        var distance = _euclidean.Distance(i - x, j - y);
                        totalDistance += colorMetric.Distance(t1.GetPixel(i, j), t2.GetPixel(x, y)) / ((distance + 1) * (distance + 1));
                    }
                }
            }
            return totalDistance;
        }
    }
}
