using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace PicturePickle
{
    // base filter class that all other filters will inherit from
    public abstract class Filter
    {
        public Color color;

        public abstract void execute(ref SKBitmap image);
    }

}
