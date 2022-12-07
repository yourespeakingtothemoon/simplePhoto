using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    internal class LinearGradient : Filter
    {
        private Color endColor;
        private float addR, addG, addB=0;
        bool vertical;
        public LinearGradient(Color col,Color end, bool vertical=true)
        {
            color = col;
            endColor = end;
            this.vertical = vertical;

        }

        public override void execute(ref SKBitmap image)
        {
            if (vertical)
            {
                addR = (endColor.zR - color.zR) / (float)image.Height;
                addG = (endColor.zG - color.zG) / (float)image.Height;
                addB = (endColor.zB - color.zB) / (float)image.Height;
                for (int y = 0; y < image.Height; y++)
                {

                    for (int x = 0; x < image.Width; x++)
                    {
                        Color pixelColor = new Color(image.GetPixel(x, y));
                        pixelColor.Multiply(ref color); // using the colors in 0-1 space we can multiply them together to combine them
                        image.SetPixel(x, y, pixelColor.ToSKColor(false));
                        // color.zR += addR / (float)image.Height;
                        //color.zG += addG / (float)image.Height;
                        //color.zB += addB / (float)image.Height;
                    }
                    color.zR += addR;
                    color.zG += addG;
                    color.zB += addB;
                }
            }
            else
            {
                addR = (endColor.zR - color.zR) / (float)image.Width;
                addG = (endColor.zG - color.zG) / (float)image.Width;
                addB = (endColor.zB - color.zB) / (float)image.Width;
                for (int x = 0; x < image.Width; x++)
                {

                    for (int y = 0; y < image.Height; y++)
                    {
                        Color pixelColor = new Color(image.GetPixel(x, y));
                        pixelColor.Multiply(ref color); // using the colors in 0-1 space we can multiply them together to combine them
                        image.SetPixel(x, y, pixelColor.ToSKColor(false));
                        // color.zR += addR / (float)image.Height;
                        //color.zG += addG / (float)image.Height;
                        //color.zB += addB / (float)image.Height;
                    }
                    color.zR += addR;
                    color.zG += addG;
                    color.zB += addB;
                }
            }
        }

        public override string toString()
        {
            return "Multiplies a gradient by a gradient of " + "[Red: " + color.r + " Green: " + color.g + " Blue: " + color.b + "]" + " and " + "[Red: " + endColor.r + " Green: " + endColor.g + " Blue: " + endColor.b + "]";
        }
    }
}
