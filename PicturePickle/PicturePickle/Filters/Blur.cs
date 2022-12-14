using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    internal class Blur: Filter
    {
        private int ammount, times;
        public Blur(int ammount, int times)
        { 
            this.ammount = ammount;
            this.times = times;
        }
        public override void execute(ref SKBitmap image)
        {
            for (int i = 0; i < times; i++)
            { 
                SKBitmap buffer = new SKBitmap((int)(image.Width / (ammount / times)), (int)(image.Height / (ammount / times)));
                image.ScalePixels(buffer, SKFilterQuality.High); // scale the image up and down to automatically average the colors
                buffer.ScalePixels(image, SKFilterQuality.High);
                
            }
        }

        public override string toString()
        {
            return "Bluring by a factor of " + ammount + "/" + times + ".";
        }
    }
}
