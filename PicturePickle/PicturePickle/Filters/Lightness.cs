using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace PicturePickle
{
    internal class Lightness: Filter
    {
        private float val;
       public Lightness(float v)
        {
            this.val = v;
        }

        public override void execute(ref SKBitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    SKColor newColor = pixelColor.ToSKColor(false);
                    float h;
                    float s;
                    float l;
                    newColor.ToHsl(out h,out s,out l);
                    l=Math.Min(100,l * val);
                    newColor = SKColor.FromHsl(h,s,Math.Max(0,l));  
                    image.SetPixel(x, y, newColor);
                }
            }
        }
    }
}
