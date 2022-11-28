using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    internal class Multiply : Filter
    {
        public Multiply(Color col)
        {
            color = col;
        }

        public override void execute(ref SKBitmap image)
        {

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {

                    Color pixelColor = new Color(image.GetPixel(x, y));
                    pixelColor.Multiply(ref color);
                    //Color.ToByte(ref newColor);
                    image.SetPixel(x, y, pixelColor.ToSKColor(false));
                    //Console.WriteLine("Pixel Done!");

                }
            }
        }
    }
}
