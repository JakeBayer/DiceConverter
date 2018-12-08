using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceConverter.Constants
{
    public static class DiceTiles
    {
        private static readonly Color B = Color.Black;
        private static readonly Color W = Color.White;

        private static readonly Tile One = new Tile(new Color[]
        {
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, B, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
        });

        private static readonly Tile Two = new Tile(new Color[]
        {
            W, W, W, W, W, W, W,
            W, W, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, W, W,
            W, W, W, W, W, W, W,
        });

        private static readonly Tile Two_R = new Tile(new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, B, W,
            W, W, W, W, W, W, W,
        });
        private static readonly Tile Three = new Tile(new Color[]
        {
            W, W, W, W, W, W, W,
            W, W, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, B, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, W, W,
            W, W, W, W, W, W, W,
        });
        private static readonly Tile Three_R = new Tile(new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, B, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, B, W,
            W, W, W, W, W, W, W,
        });
        private static readonly Tile Four = new Tile(new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
        });
        private static readonly Tile Five = new Tile(new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, B, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
        });
        private static readonly Tile Six = new Tile(new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
            W, B, W, W, W, B, W,
            W, W, W, W, W, W, W,
        });
        private static readonly Tile Six_R = new Tile(new Color[]
        {
            W, W, W, W, W, W, W,
            W, B, W, B, W, B, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, W, W, W, W, W, W,
            W, B, W, B, W, B, W,
            W, W, W, W, W, W, W,
        });

        public static IEnumerable<Tile> Tiles { get; } = new Tile[]
        {
            One, Two, Two_R, Three, Three_R, Four, Five, Six, Six_R
        };
    }
}
