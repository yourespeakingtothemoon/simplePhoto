using System;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.FormsBook.Toolkit;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;
using SkiaSharp;

namespace simplePhoto.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        Pixel[,] pixels;

        /// <summary>
        /// When a user presses the button, they are prompted to upload an image.
        /// Afterwards the image is displayd on the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void uploadButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    string Text = $"File name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        var stream2 = await result.OpenReadAsync();
                        MainImage.Source = ImageSource.FromStream(() => stream);
                        SKBitmap skb = new SKBitmap();
                        skb = SKBitmap.Decode(stream2);
                        byte[] bytes = skb.Bytes;
                        pixels = Pixel.ToPixelArray(bytes, skb.Width, skb.Height);
                        AddColor(ref pixels);
                        string str = "";
                        foreach (var p in pixels)
                        {
                            str += p.tostr() + "\n";
                        }
                        Label lab = new Label() { Text = str };
                        layout.Children.Add(lab);
                        FileName.Text = Text;
                    }
                    else
                    {
                        FileName.Text = "Incorrect file type. Please enter a .png or .jpg file.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddColor(ref Pixel[,] pixs)
        {
            for (int i = 0; i < pixs.GetLength(0); i++)
            {
                for (int j = 0; j < pixs.GetLength(1); j++)
                {
                    pixs[i, j].r += 30;
                    pixs[i, j].g += 30;
                    pixs[i, j].b += 30;
                }
            }
        }

        public class Pixel 
        {
            public int r, g, b, a;
            public Pixel(int r, int g, int b, int a)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }

            public static Pixel[,] ToPixelArray(byte[] bytes, int width, int height)
            {
                Pixel[,] pixs = new Pixel[height, width];
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int start = (i * width + j) * 4;
                        pixs[i, j] = new Pixel(bytes[start + 0], bytes[start + 1], bytes[start + 2], bytes[start + 3]);
                    }
                }
                return pixs;
            }

            public static byte[] ToByteArray(ref Pixel[,] pixels)
            {
                byte[] bytes = new byte[pixels.GetLength(0) * pixels.GetLength(1) * 4];
                for (int i = 0; i < bytes.Length; i += 4)
                {
                    int height = i % pixels.GetLength(0);
                    int width = height ;
                    bytes[i + 0] = (byte)pixels[height, i % pixels.GetLength(1)].r;
                    bytes[i + 1] = (byte)pixels[height, i % pixels.GetLength(1)].g;
                    bytes[i + 2] = (byte)pixels[height, i % pixels.GetLength(1)].b;
                    bytes[i + 3] = (byte)pixels[height, i % pixels.GetLength(1)].a;
                }

                return bytes;
            }

            public string tostr()
            {
                return $"r:{r}, g:{g}, b{b}, a{a}";
            }
        }
        
    }
}