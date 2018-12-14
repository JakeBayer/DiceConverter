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

        public double getD(int x)
        {
            return Math.Round((double)x / 6, 2);
        }


        public Bitmap Tile<TTileComparer>(Bitmap original, IEnumerable<Bitmap> tileImages, TTileComparer comparer)
            where TTileComparer : ITileComparer
        {
            var tiles = tileImages.Select(t => new Tile(t)).ToList();

            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            var tileSize = tiles.First().Width;
            var imageWidth = original.Width;
            var imageHeight = original.Height;

            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                for (int i = 0; i < imageWidth; i += tileSize)
                {
                    for (int j = 0; j < imageHeight; j += tileSize)
                    {
                        Tile closest;
                        var tile = new Tile(original.Clone(new Rectangle(i, j, tileSize, tileSize), original.PixelFormat));
                        double brightness = Math.Round(tile.GetAverageColorAt(0,0,7).GetBrightness(), 2);
                        if (brightness <= getD(1))
                        {
                            var terls = tiles.Skip(7).Take(2).ToList();
                            for (int x = 0; x < 5; x++)
                            {
                                for (int y = 0; y < 5; y++)
                                {
                                    foreach (var terl in terls)
                                    {
                                        var p = terl.GetAverageColorAt(x, y, 3);
                                    }
                                }
                            }
                            foreach (var terl in terls)
                            {
                                for (int x = 0; x < 5; x++)
                                {
                                    for (int y = 0; y < 5; y++)
                                    {
                                        var p = terl.GetAverageColorAt(x, y, 3);
                                        Console.WriteLine($"x: {x} \ty: {y} \tb={p.R}");
                                    }
                                }
                            }

                            var ranked = tiles.Skip(7).Take(2).Select(t => Tuple.Create(t, comparer.GetTileDifference(tile, t, _distanceMetric, _colorDistanceMetric))).ToList();
                            closest = ranked.OrderBy(r => r.Item2).First().Item1;
                            //closest = tiles.Skip(7).Take(2).OrderBy(t => comparer.GetTileDifference(tile, t, _distanceMetric, _colorDistanceMetric)).First();
                        }
                        else if (brightness > getD(1) && brightness <= getD(2))
                            closest = tiles[6];
                        else if (brightness > getD(2) && brightness <= getD(3))
                            closest = tiles[5];
                        else if (brightness > getD(3) && brightness <= getD(4))
                            closest = tiles.Skip(3).Take(2).OrderBy(t => comparer.GetTileDifference(tile, t, _distanceMetric, _colorDistanceMetric)).First();
                        else if (brightness > getD(4) && brightness <= getD(5))
                            closest = tiles.Skip(1).Take(2).OrderBy(t => comparer.GetTileDifference(tile, t, _distanceMetric, _colorDistanceMetric)).First();
                        else if (brightness > getD(5))
                            closest = tiles[0];
                        else
                        {
                            throw new InvalidOperationException("Shouldn't ever happen");
                        }
                        //var scored = tilesAsBmp.Select(t => comparer.GetTileDifference(tile, t)).ToArray();
                        //var closest = tiles.OrderBy(t => comparer.GetTileDifference(tile, t, _distanceMetric, _colorDistanceMetric)).First();

                        g.DrawImage(closest.Image, new Rectangle(i, j, tileSize, tileSize));
                    }
                }
            }

            return newBitmap;
        }
    }
}
