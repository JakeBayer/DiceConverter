using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using DiceConverter.ColorComparer;
using DiceConverter.Distance;
using DiceConverter.Extensions;
using DiceConverter.PreProcess;
using DiceConverter.TileComparer;

namespace DiceConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string imagePath = @"C:\git\DiceConverter\image9.jpg";

            var bmp = new Bitmap(imagePath);

            var img = ImagePreProcessor.MakeMultipleOfN(bmp, 7);

            pictureBox1.Image = img;

            var greybmp = ImagePreProcessor.MakeGreyscale(img);

            greybmp.Save(@"C:\git\DiceConverter\image4_grey.jpg");

            pictureBox2.Image = greybmp;


            //var pointifiedImage = ImagePreProcessor.Pointify(greybmp);
            //pointifiedImage.Save(@"C:\git\DiceConverter\image4_adj_grey_pointified.jpg");

            var imageAvgColor = ImagePreProcessor.CaluclateAverageColor(greybmp);

            var adjustedGrey = ImagePreProcessor.AdjustBrightness(greybmp, 280 - imageAvgColor.R);

            adjustedGrey.Save(@"C:\git\DiceConverter\image4_adj_grey.jpg");

            var tiler = new ImageTiler(new EuclideanMetric(), new EuclideanColorComparer());

            var diceTiles = Constants.DiceTiles.Tiles.Select(BitmapFactory.FromColorArray);

            var diveAvg = diceTiles.Select(ImagePreProcessor.CaluclateAverageColor).ToList();

            var dice = tiler.Tile(adjustedGrey, diceTiles, new SlidingTileComparer(3));
            pictureBox3.Image = dice;

            dice.Save(@"C:\git\DiceConverter\image4_dice.jpg");
        }
    }
}
