using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace PicturePickle
{
    internal class Add: Filter
    {
       public Add(Color col)
        {
            this.color = col;
        }

        public override void execute(ref SKBitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    pixelColor.Add(ref color); // directly add the values of the color
                    image.SetPixel(x, y, pixelColor.ToSKColor(false));
                }
            }
        }
    }
}
