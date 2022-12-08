using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    internal class TwoTone : Filter
    {
        private Color col1;
        private Color col2;
        private int cutoff;

        public TwoTone(Color col1, Color col2, int cuttoff)
        {
            this.col1 = new Color(col1);
            this.col2 = new Color(col2);
            this.cutoff = cuttoff;
        }

        public override void execute(ref SKBitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    Color grayColor = new Color((int)pixelColor.Value());
                    Color co1 = new Color(col1);
                    Color co2 = new Color(col2);

                    if (pixelColor.Value() < cutoff)
                    {
                        grayColor.Invert();
                        co1.Multiply(ref grayColor);
                        pixelColor = new Color(co1);
                    }
                    else
                    {
                        co2.Multiply(ref grayColor);
                        pixelColor = new Color(co2);
                    }
                    image.SetPixel(x, y, pixelColor.ToSKColor(false));
                }
            }
        }

        public override string toString()
        {
            return "Sets two color tones of " + "[Red: " + col1.r + " Green: " + col1.g + " Blue: " + col1.b + "]" + " and " + "[Red: " + col2.r + " Green: " + col2.g + " Blue: " + col2.b + "]";
        }
    }
}
