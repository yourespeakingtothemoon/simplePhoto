using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    internal class Grayscale : Filter
    {
        public override void execute(ref SKBitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    pixelColor.Set((int)pixelColor.Value()); // set each pixel to the value of that pixel
                    image.SetPixel(x, y, pixelColor.ToSKColor(false));
                }
            }
        }
        public override string toString()
        {
            return "Changing image to monochrome.";
        }
    }
   
}
