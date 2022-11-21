using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace PicturePickle
{
    // color class because hsv is stinky
    internal class Color
    {
        // these will be public because i dont care about encapsulation 
        public int r, g, b, a;
        public float zR, zG, zB, zA;

        /// <summary>
        /// Use the colors as floats to multiply the colors together
        /// </summary>
        /// <param name="col"></param>
        /// <returns>The new multiplied color</returns>
        public Color Multiply(ref Color col) 
        {
            return new Color(zR * col.zR, zG * col.zG, zB * col.zB, zA);
        }

        /// <summary>
        /// Add a color to another color
        /// </summary>
        /// <param name="col"></param>
        /// <returns>The new added Color</returns>
        public Color Add(ref Color col, bool alpha = false)
        {
            if (alpha)
            {
                return new Color(r + col.r, g + col.g, b + col.b, a + col.a);
            }
            else
            {
                return new Color(r + col.r, g + col.g, b + col.b, a);
            }
        }

        /// <summary>
        /// Convert the color to an SKColor
        /// </summary>
        /// <param name="scaled">Should the values be scaled or capped</param>
        /// <returns>The color as an SKColor</returns>
        public SKColor ToSKColor(bool scaled = true)
        {
            if (scaled)
            {
                float max = Math.Max(Math.Max(zR, zG), zB);
                return new SKColor((byte)((zR / max) * 255), (byte)((zB / max) * 255), (byte)((zG / max) * 255), (byte)(zA * 255));
            }
            else
            { 
                if (r > 255) r = 255;
                if (r < 0) r = 0;
                if (g > 255) g = 255;
                if (g < 0) g = 0;
                if (b > 255) b = 255;
                if (b < 0) b = 0;
                if (a > 255) a = 255;
                if (a < 0) a = 0;
                return new SKColor((byte)r, (byte)g, (byte)b, (byte)a);
            }
        }


        //*** CONSTRUCTORS ***//
        public Color()
        {
            r = 0;
            g = 0;
            b = 0;
            a = 0;
            toZeroToOne();
        }

        public Color(int c)
        {
            r = c;
            g = c;
            b = c;
            a = 255;
            toZeroToOne();
        }

        public Color(int r, int g, int b, int a)
        { 
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
            toZeroToOne();
        }

        public Color(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 255;
            toZeroToOne();
        }

        public Color(float r, float g, float b, float a)
        {
            zR = r;
            zG = g;
            zB = b;
            zA = a;
            toInts();
        }

        public Color(float r, float g, float b)
        {
            zR = r;
            zG = g;
            zB = b;
            zA = 1.0f;
            toInts();
        }

        public Color(SKColor col)
        {
            r = col.Red;
            g = col.Green;
            b = col.Blue;
            a = col.Alpha;
            toZeroToOne();
        }

        public Color(Color col)
        {
            r = col.r;
            g = col.g;
            b = col.b;
            a = col.a;
            toZeroToOne();
        }

        /// <summary>
        /// Converts the int colors into floats
        /// </summary>
        private void toZeroToOne()
        {
            zR = (float)r / 255.0f;
            zG = (float)g / 255.0f;
            zB = (float)b / 255.0f;
            zA = (float)a / 255.0f;
        }

        /// <summary>
        /// Converts the float colors to ints
        /// </summary>
        private void toInts()
        {
            r = (int)(zR * 255.0f);
            g = (int)(zG * 255.0f);
            b = (int)(zB * 255.0f);
            a = (int)(zA * 255.0f);
        }
    }
}
