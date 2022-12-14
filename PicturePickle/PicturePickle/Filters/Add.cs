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
            color = new Color(col);
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

        public override string toString()
        {
            return "Adding Color " + "[Red: " + color.r + " Green: " + color.g + " Blue: " + color.b + "]" + "to the image.";
        }
    }
}
