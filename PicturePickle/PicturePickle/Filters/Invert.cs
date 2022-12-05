using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    class Invert : Filter
    {
        public Invert() { }

        public override void execute(ref SKBitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    pixelColor.Invert(); // set each pixel to the inverse of that pixel
                    image.SetPixel(x, y, pixelColor.ToSKColor(false));
                }
            }
        }
        public override string toString()
        {
            return "Invert the colors of the image.";
        }
    }
}
