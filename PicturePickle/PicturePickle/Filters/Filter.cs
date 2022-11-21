using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace PicturePickle
{
    // base filter class that all other filters will inherit from
    public abstract class Filter
    {
        public abstract void addLayer(Color color,int behavior);
        public abstract void removeLayer(Color color,int behavior);

        public Color[] colors;

        public int[] behaviors;

        public abstract SKColor execute(Color pixColor);
    }
}
