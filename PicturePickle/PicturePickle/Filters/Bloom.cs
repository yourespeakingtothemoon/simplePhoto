using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    internal class Bloom : Filter
    {
        private int threshold;
        public Bloom(int threshold = 220)
        { 
            this.threshold = threshold;
        }

        public override void execute(ref SKBitmap image)
        {
            SKBitmap buffer = new SKBitmap();
            image.CopyTo(buffer);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    if ((int)pixelColor.Value() < threshold)
                    {
                        pixelColor.Set(0); // find the pixels that are above a certain threashold and set all others to 0
                    }
                    buffer.SetPixel(x, y, pixelColor.ToSKColor(false));
                }
            }

            for (int i = 0; i < 5; i++)
            {
                SKBitmap sBuffer = new SKBitmap((int)(image.Width / (20 / 5)), (int)(image.Height / (20 / 5)));
                buffer.ScalePixels(sBuffer, SKFilterQuality.High); // blur the image that was comprised of only bright pixels
                sBuffer.ScalePixels(buffer, SKFilterQuality.High);
            }

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    Color bufferColor = new Color(buffer.GetPixel(x, y));
                    pixelColor.Add(ref bufferColor); // add the bright pixels back into the orinal image
                    image.SetPixel(x, y, pixelColor.ToSKColor(false));
                }
            }
        }

        public override string toString()
        {
            return "Creating Bloom effect with " + threshold+" as the white threshold.";
        }
    }
}
