using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Effects;

namespace PicturePickle
{
    class Kuwahara : Filter
    {
        private int kernel;
        public Kuwahara(int kernel = 4)
        {
            this.kernel = kernel;
        }

        public override void execute(ref SKBitmap image)
        {
            SKBitmap buffer = new SKBitmap();
            image.CopyTo(buffer);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    sector snn = new sector();
                    sector snp = new sector();
                    sector spn = new sector();
                    sector spp = new sector();

                    snn.color = new List<Color>();
                    snp.color = new List<Color>();
                    spn.color = new List<Color>();
                    spp.color = new List<Color>();

                    for(int i = 0; i <= kernel; i++) 
                    {
                        for (int j = 0; j <= kernel; j++)
                        {
                            if (x - i >= 0)
                            {
                                if (y - j >= 0)
                                {
                                    snn.color.Add(new Color(image.GetPixel(x - i, y - j)));
                                }
                                if (y + j < image.Height)
                                {
                                    snp.color.Add(new Color(image.GetPixel(x - i, y + j)));
                                }
                            }
                            else if (x + i < image.Width)
                            {
                                if (y - j >= 0)
                                {
                                    spn.color.Add(new Color(image.GetPixel(x + i, y - j)));
                                }
                                if (y + j < image.Height)
                                {
                                    spp.color.Add(new Color(image.GetPixel(x + i, y + j)));
                                }
                            }
                        }
                    }

                    AvgColor(ref snn);
                    AvgColor(ref snp);
                    AvgColor(ref spn);
                    AvgColor(ref spp);

                    float lsdv = Math.Min(snn.standardDeviation, Math.Min(snp.standardDeviation, Math.Min(spn.standardDeviation, spp.standardDeviation)));
                    if(snn.standardDeviation == lsdv && snn.color.Count > kernel) 
                    { 
                        buffer.SetPixel(x, y, snn.averageColor.ToSKColor(false));
                    }
                    if (snp.standardDeviation == lsdv && snp.color.Count > kernel)
                    {
                        buffer.SetPixel(x, y, snp.averageColor.ToSKColor(false));
                    }
                    if (spn.standardDeviation == lsdv && spn.color.Count > kernel)
                    {
                        buffer.SetPixel(x, y, spn.averageColor.ToSKColor(false));
                    }
                    if (spp.standardDeviation == lsdv && spp.color.Count > kernel)
                    {
                        buffer.SetPixel(x, y, spp.averageColor.ToSKColor(false));
                    }
                }
            }
            buffer.CopyTo(image);
        }

        struct sector
        {
            public List<Color> color;
            public Color averageColor;
            public float standardDeviation;
        }

        private void AvgColor(ref sector s)
        {
            Color[] colors = s.color.ToArray();
            Color avg = new Color();
            float sdv = float.MaxValue;
            for (int i = 0; i < s.color.Count; i++)
            {
                avg.Add(ref colors[i]);
                if (sdv == float.MaxValue)
                {
                    sdv = colors[i].Value();
                }
                else 
                { 
                    sdv += colors[i].Value();
                }
            }
            if (colors.Length > 0)
            {
                avg.r /= colors.Length;
                avg.g /= colors.Length;
                avg.b /= colors.Length;
                avg.a = 255;
                s.averageColor = avg;
                s.standardDeviation = sdv / colors.Length;
                List<float> diff = new List<float>();
                for (int i = 0; i < s.color.Count; i++)
                {
                    diff.Add((float)Math.Pow((colors[i].Value() - s.standardDeviation), 2));
                }
                float mosd = 0;
                foreach (float f in diff)
                { 
                    mosd += f;
                }
                mosd /= diff.Count;
                Math.Sqrt(mosd);
                s.standardDeviation = mosd;
            }
            else
            { 
                s.standardDeviation = float.MaxValue;
            }
        }

        public override string toString()
        {
            return "Applying Kuwahara (Painted) effect using a kernel size of "+kernel+".";
        }
    }
}
