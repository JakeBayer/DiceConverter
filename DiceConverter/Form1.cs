using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiceConverter.ColorComparer;
using DiceConverter.Distance;
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
            string imagePath = @"C:\git\DiceConverter\image4.jpg";

            var bmp = new Bitmap(imagePath);

            var img = MakeMultipleOfN(bmp, 7);

            pictureBox1.Image = img;

            var greybmp = MakeGreyscale(img);

            greybmp.Save(@"C:\git\DiceConverter\image4_grey.jpg");

            pictureBox2.Image = greybmp;

            var tiler = new ImageTiler(new TaxiMetric(), new EuclideanColorComparer());
            pictureBox3.Image = tiler.Tile(greybmp, Constants.DiceTiles.Tiles, new FuzzyTileComparer(1));
        }

        public Bitmap MakeMultipleOfN(Bitmap original, int n)
        {
            
            int newWidth  = original.Width  - (original.Width  % n),
                newHeight = original.Height - (original.Height % n);
            return original.Clone(new Rectangle(0, 0, newWidth, newHeight), original.PixelFormat);
        }

        public static Bitmap MakeGreyscale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                    new float[][]
                    {
                        new float[] { .3f, .3f, .3f, 0, 0 },
                        new float[] { .59f, .59f, .59f, 0, 0 },
                        new float[] { .11f, .11f, .11f, 0, 0 },
                        new float[] { 0, 0, 0, 1, 0 },
                        new float[] { 0, 0, 0, 0, 1 }
                    });

                //create some image attributes
                ImageAttributes attributes = new ImageAttributes();

                //set the color matrix attribute
                attributes.SetColorMatrix(colorMatrix);

                //draw the original image on the new image
                //using the greyscale color matrix
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }
            return newBitmap;
        }
    }
}
