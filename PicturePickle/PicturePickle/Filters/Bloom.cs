using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    internal class Bloom : Filter
    {
        public override void execute(ref SKBitmap image)
        {
            SKBitmap buffer = new SKBitmap();
            image.CopyTo(buffer);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    if ((int)pixelColor.Value() < 220)
                    {
                        pixelColor.Set(0);
                    }
                    buffer.SetPixel(x, y, pixelColor.ToSKColor(false));
                }
            }

            for (int i = 0; i < 5; i++)
            {
                SKBitmap sBuffer = new SKBitmap((int)(image.Width / (20 / 5)), (int)(image.Height / (20 / 5)));
                buffer.ScalePixels(sBuffer, SKFilterQuality.High);
                sBuffer.ScalePixels(buffer, SKFilterQuality.High);
            }

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    Color bufferColor = new Color(buffer.GetPixel(x, y));
                    pixelColor.Add(ref bufferColor);
                    image.SetPixel(x, y, pixelColor.ToSKColor(false));
                }
            }
        }
    }
}
