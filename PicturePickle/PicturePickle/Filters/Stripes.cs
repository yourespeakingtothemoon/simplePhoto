using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    internal class Stripes : Filter
    {
        private Color endColor;
        private float addR, addG, addB=0;
        Random rnd = new Random();
        //bool vertical;
        public Stripes()
        {
            color = new Color(rnd.Next(0,255), rnd.Next(0, 255), rnd.Next(0, 255));
            endColor = new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            //this.vertical = vertical;

        }

        public override void execute(ref SKBitmap image)
              
        {
          
            addR = (endColor.zR - color.zR) / (float)image.Height;
                addG = (endColor.zG - color.zG) / (float)image.Height;
                addB = (endColor.zB - color.zB) / (float)image.Height;
                for  (int x = 0; x < image.Width; x++)
                    
                {

                    for (int y = 0; y < image.Height; y++)
                { 
                        Color pixelColor = new Color(image.GetPixel(x, y));
                        pixelColor.Multiply(ref color); // using the colors in 0-1 space we can multiply them together to combine them
                        image.SetPixel(x, y, pixelColor.ToSKColor(false));
                        color.zR += addR / (float)image.Height;
                        color.zG += addG / (float)image.Height;
                        color.zB += addB / (float)image.Height;
                    }
                // color.zR += addR;
                //color.zG += addG;
                //color.zB += addB;
                color.zR = (float)rnd.NextDouble();
                color.zG = (float)rnd.NextDouble();
                color.zB = (float)rnd.NextDouble();
            }
        
            }
        

        public override string toString()
        {
            return "Adds random stripes to the image"; 
        }
    }
}
