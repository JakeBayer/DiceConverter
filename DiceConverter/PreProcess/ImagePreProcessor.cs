using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceConverter.PreProcess
{
    public static class ImagePreProcessor
    {
        public static Color CaluclateAverageColor(Bitmap image)
        {
            double         r = 0, g = 0, b = 0,
                       r2, g2,    b2;
            long num = 0;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var    pixel = image.GetPixel(i, j);
                    double fudge = (double)num / (double)(num + 1);
                    r2 = pixel.R * pixel.R;
                    g2 = pixel.G * pixel.G;
                    b2 = pixel.B * pixel.B;
                    r  = r * fudge + r2 / (double)(num + 1);
                    g  = g * fudge + g2 / (double)(num + 1);
                    b  = b * fudge + b2 / (double)(num + 1);
                    num++;
                }
            }
            return Color.FromArgb((int)Math.Sqrt(r), (int)Math.Sqrt(g), (int)Math.Sqrt(b));
        }

        public static Bitmap AdjustBrightness(Bitmap image, int value)
        {

            Bitmap TempBitmap = image;

            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);

            Graphics NewGraphics = Graphics.FromImage(NewBitmap);

            float FinalValue = (float)value / 255.0f;

            float[][] FloatColorMatrix =
            {

                new float[] { 1, 0, 0, 0, 0 },

                new float[] { 0, 1, 0, 0, 0 },

                new float[] { 0, 0, 1, 0, 0 },

                new float[] { 0, 0, 0, 1, 0 },

                new float[] { FinalValue, FinalValue, FinalValue, 1, 1 }
            };

            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);

            ImageAttributes Attributes = new ImageAttributes();

            Attributes.SetColorMatrix(NewColorMatrix);

            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), 0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);

            Attributes.Dispose();

            NewGraphics.Dispose();

            return NewBitmap;
        }

        public static Bitmap MakeMultipleOfN(Bitmap original, int n)
        {

            int newWidth  = original.Width - (original.Width % n),
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

        public static Bitmap Pointify(Bitmap image)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < image.Width; x += 7)
            {
                for (int y = 0; y < image.Height; y += 7)
                {
                    for (int i = 1; i < 7; i += 2)
                    {
                        for (int j = 1; j < 7; j += 2)
                        {
                            newBitmap.SetPixel(x+i, y+j, AverageColorAt(image, x+i, y+j));
                        }
                    }
                }
            }
            return newBitmap;
        }

        private static Color AverageColorAt(Bitmap image, int x, int y)
        {
            double r = 0, g = 0, b = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    var pixel = image.GetPixel(x + i, y + j);
                    r += pixel.R * pixel.R;
                    g += pixel.G * pixel.G;
                    b += pixel.B * pixel.B;
                }
            }
            var num = 9;
            return Color.FromArgb((int)Math.Sqrt(r / num), (int)Math.Sqrt(g / num), (int)Math.Sqrt(b / num));
        }
    }
}
