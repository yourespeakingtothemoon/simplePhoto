using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace PicturePickle.Filters
{
    internal class Add: Filter
    {

        public override void execute(ref SKBitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {

                    Color pixelColor = new Color(image.GetPixel(x, y));
                    Color newColor = new Color(pixelColor);
                    pixelColor.Add(ref newColor);
                    //Color.ToByte(ref newColor);
                    image.SetPixel(x, y, newColor.ToSKColor());
                    //Console.WriteLine("Pixel Done!");

                }
            }
        }
    }
}
