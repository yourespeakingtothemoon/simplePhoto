using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace PicturePickle
{
    internal class ValueAdj: Filter
    {
        private float val;
       public ValueAdj(float v)
        {
            this.val = v;
        }

        public override void execute(ref SKBitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    //works best for darkening can also have an inverse effect on the color if negative float is plugged in
                    Color pixelColor = new Color(image.GetPixel(x, y));
                    SKColor newColor = pixelColor.ToSKColor(false);
                    float h;
                    float s;
                    float v;
                    newColor.ToHsv(out h,out s,out v);
                    v=Math.Min(100,v * val);
                    newColor = SKColor.FromHsv(h,s,v);  
                    image.SetPixel(x, y, newColor);
                }
            }
        }

        public override string toString()
        {
            return "Adjusting HSV Color Value by a factor of " + val + ".";
        }
    }
}
