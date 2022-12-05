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
                    pixelColor.Multiply(ref color); // using the colors in 0-1 space we can multiply them together to combine them
                    image.SetPixel(x, y, pixelColor.ToSKColor(false));
                }
            }
        }

        public override string toString()
        {
            return "Multiplying image by the Color values: " + "[Red: " + color.zR.ToString() + " Green: " + color.zG.ToString() + " Blue: " + color.zB.ToString() + "]" + ".";
        }
    }
}
