using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceConverter.Distance;
using DiceConverter.Extensions;
using DiceConverter.TileComparer;

namespace DiceConverter
{
    public class ImageTiler
    {
        private readonly IDistanceMetric<int> _distanceMetric;
        private readonly IDistanceMetric<Color> _colorDistanceMetric;

        public ImageTiler(IDistanceMetric<int> distanceMetric, IDistanceMetric<Color> colorDistanceMetric)
        {
            _colorDistanceMetric = colorDistanceMetric;
            _distanceMetric = distanceMetric;
        }

        public Bitmap Tile<TTileComparer>(Bitmap original, IEnumerable<Tile> tiles, TTileComparer comparer)
            where TTileComparer : ITileComparer
        {
            var tilesAsBmp = tiles.Select(t => BitmapFactory.FromColorArray(t.Pixels)).ToList();

            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            var tileSize = tiles.First().Pixels.GetLength(0);
            var imageWidth = original.Width;
            var imageHeight = original.Height;

            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                for (int i = 0; i < imageWidth; i += tileSize)
                {
                    for (int j = 0; j < imageHeight; j += tileSize)
                    {
                        var tile = original.Clone(new Rectangle(i, j, tileSize, tileSize), original.PixelFormat);
                        //var scored = tilesAsBmp.Select(t => comparer.GetTileDifference(tile, t)).ToArray();
                        var closest = tilesAsBmp.OrderBy(t => comparer.GetTileDifference(tile, t, _distanceMetric, _colorDistanceMetric)).First();

                        g.DrawImage(closest, new Rectangle(i, j, tileSize, tileSize));
                    }
                }
            }

            return newBitmap;
        }
    }
}
