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
            //string imagePath = @"C:\git\DiceConverter\image14.jpg";

            //var bmp = new Bitmap(imagePath);

            //var img = ImagePreProcessor.MakeMultipleOfN(bmp, 7);

            //pictureBox1.Image = img;

            //var greybmp = ImagePreProcessor.MakeGreyscale(img);

            //greybmp.Save(@"C:\git\DiceConverter\image4_grey.jpg");

            //pictureBox2.Image = greybmp;


            ////var pointifiedImage = ImagePreProcessor.Pointify(greybmp);
            ////pointifiedImage.Save(@"C:\git\DiceConverter\image4_adj_grey_pointified.jpg");

            //var imageAvgColor = ImagePreProcessor.CaluclateAverageColor(greybmp);

            //var adjustedGrey = ImagePreProcessor.AdjustBrightness(greybmp, 280 - imageAvgColor.R);

            //adjustedGrey.Save(@"C:\git\DiceConverter\image4_adj_grey.jpg");

            //var tiler = new ImageTiler(new EuclideanMetric(), new EuclideanColorComparer());

            //var diceTiles = Constants.DiceTiles.Tiles.Select(BitmapFactory.FromColorArray);

            //var diveAvg = diceTiles.Select(ImagePreProcessor.CaluclateAverageColor).ToList();

            //var dice = tiler.Tile(adjustedGrey, diceTiles, new SlidingTileComparer(3));
            //pictureBox3.Image = dice;

            //dice.Save(@"C:\git\DiceConverter\image4_dice.jpg");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog { RestoreDirectory = true };
            string fileNameNoExt = "";
            string filePath      = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                fileNameNoExt = System.IO.Path.GetFileNameWithoutExtension(path);
                filePath      = System.IO.Path.GetDirectoryName(path);
                tbPath.Text   = path;
            }
            else
            {
                return;
            }
            await Task.Run(() =>
            {
                try
                {
                    Bitmap bmp     = (Bitmap) Image.FromFile(tbPath.Text);
                    var    img     = ImagePreProcessor.MakeMultipleOfN(bmp, 7);
                    var    greybmp = ImagePreProcessor.MakeGreyscale(img);
                    var imageAvgColor = ImagePreProcessor.CaluclateAverageColor(greybmp);

                    var avgBrightness = imageAvgColor.GetBrightness();

                    var adjustedGrey = ImagePreProcessor.AdjustBrightness(greybmp, 0);
                    pictureBox3.Image = adjustedGrey;

                    var tiler = new ImageTiler(new EuclideanMetric(), new BrightnessColorComparer());

                    var diceTiles = Constants.DiceTiles.Tiles.Select(BitmapFactory.FromColorArray);

                    var dice = tiler.Tile(adjustedGrey, diceTiles, new SlidingTileComparer(3));

                    dice.Save(filePath + "\\" + fileNameNoExt + "#DiceImage#.png", ImageFormat.Png);
                    pictureBox3.Image = dice;
                }
                catch (ArgumentException err)
                {
                    MessageBox.Show("Cannot generate a dice preview for this image because it is too big!" + Environment.NewLine + Environment.NewLine + "Try scaling it down!",
                        "Resize Image for Dice Preview! (Make it smaller!)");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            });
        }
    }
}
