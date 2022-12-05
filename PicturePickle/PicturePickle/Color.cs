using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace PicturePickle
{
    // color class because hsv is stinky
    public class Color
    {
        // these will be public because i dont care about encapsulation 
        public int r, g, b, a; // 0-255 representation
        public float zR, zG, zB, zA; // 0-1 representation

        /// <summary>
        /// Use the colors as floats to multiply the colors together
        /// </summary>
        /// <param name="col"></param>
        /// <returns>The new multiplied color</returns>
        public void Multiply(ref Color col) 
        {
            zR *= col.zR;
            zG *= col.zG;
            zB *= col.zB;
            toInts();
        }

       

        /// <summary>
        /// Add a color to another color
        /// </summary>
        /// <param name="col"></param>
        /// <returns>The new added Color</returns>
        public void Add(ref Color col, bool alpha = false)
        {
            if (alpha)
            {
                r += col.r;
                g += col.g;
                b += col.b;
                a += col.a;
            }
            else
            {
                r += col.r;
                g += col.g;
                b += col.b;
            }
            toZeroToOne();
        }

        /// <summary>
        /// Set current color its negative for subtraction
        /// </summary>
        public void ToAdditiveInverse()
        {
            r = -r;
            g = -g;
            b = -b;
        }

        /// <summary>
        /// Set current color its recipricol for division
        /// </summary>
        public void ToMultiplicitiveInverse()
        {
            zR = 1 - r / 255.0f;
            zG = 1 - g / 255.0f;
            zB = 1 - b / 255.0f;
        }

        /// <summary>
        /// Set current color to another color
        /// </summary>
        /// <param name="col">Color to set</param>
        public void Set(ref Color col)
        { 
            r = col.r;
            g = col.g;
            b = col.b;
            a = col.a;
            toZeroToOne();
        }

        /// <summary>
        /// Set current color to another color
        /// </summary>
        /// <param name="c">Color to set as one 0-255 int</param>
        public void Set(int c)
        {
            r = c;
            g = c;
            b = c;
            toZeroToOne();
        }

        /// <summary>
        /// Set current color to another color
        /// </summary>
        /// <param name="c">Color to set as one 0-1 float</param>
        public void Set(float c)
        {
            zR = c;
            zG = c;
            zB = c;
            toInts();
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

        /// <summary>
        /// Get the value of the pixel
        /// </summary>
        /// <param name="alpha">Should alpha be incuded in the calculation</param>
        /// <returns>The vlaue of the pixel</returns>
        public float Value(bool alpha = false)
        {
            if (!alpha)
            {
                return ((r + g + b) * 0.33333333f);
            }
            else
            {
                return ((r + g + b + a) * 0.25f);
            }    
        }

        /// <summary>
        /// Invert the colors
        /// </summary>
        public void Invert()
        {
            r = 255 - r;
            g = 255 - g;
            b = 255 - b;
            toZeroToOne();
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

        public Color(float c)
        {
            zR = c;
            zG = c;
            zB = c;
            zA = 1.0f;
            toInts();
        }

        public Color(int r, int g, int b, int a = 255)
        { 
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
            toZeroToOne();
        }

        public Color(float r, float g, float b, float a = 1.0f)
        {
            zR = r;
            zG = g;
            zB = b;
            zA = a;
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
