using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    // base filter class that all other filters will inherit from
    public abstract class Filter
    {
        public void addLayer(Color color,int behavior);
        public void removeLayer(Color color,int behavior);

        public Color[] colors;

        public int[] behaviors;

        public Color execute(Color pixColor);
    }
}
