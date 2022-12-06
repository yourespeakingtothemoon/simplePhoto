using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    internal class Interpolate : Filter
    {
        private Color col1;
        private Color col2;

        public Interpolate(Color col1, Color col2)
        {
            this.col1 = col1;
            this.col2 = col2;
        }

        public override void execute(ref SKBitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    Color co1 = new Color(col1);
                    Color co2 = new Color(col2);

                    Color Final = pixelColor.Interpolate(ref co1, ref co2);

                    image.SetPixel(x, y, Final.ToSKColor(false));
                }
            }
        }

        public override string toString()
        {
            return "Interpolate between two different colors.";
        }
    }
}
