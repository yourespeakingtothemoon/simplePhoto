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
                        var stream2 = await result.OpenReadAsync();
                        SKBitmap skb = new SKBitmap();
                        skb = SKBitmap.Decode(stream2);
                        FileName.Text = Text;
                        Random random = new Random();
                        Color filter = new Color(29,231, 55, 0);
                        for (int x = 0; x < skb.Width; x++)
                        {
                            for (int y = 0; y < skb.Height; y++)
                            { 

                                SKColor pixelColor = skb.GetPixel(x, y);
                                Color newColor = Color.SKColorToColor(pixelColor);
                                if (newColor.value() > 200)
                                {
                                    Color.Add(ref newColor.r, -50);
                                    Color.Add(ref newColor.g, -50);
                                }
                                Color.toByte(ref newColor);
                                skb.SetPixel(x,y,Color.ColorToSKColor(newColor));
                                //Console.WriteLine("Pixel Done!");

                            }
                        }
                        SKData skd = skb.Encode(SKEncodedImageFormat.Png,100);
                        Stream stream3 = new MemoryStream();
                        stream3 = skd.AsStream();
                        MainImage.Source = ImageSource.FromStream(() => stream3);
                        /*byte[] data2 = skb.Bytes;
                        string str2 = "";
                        foreach (var item in data2)
                        {
                            str2 += item + ", ";
                        }
                        Label label2 = new Label { Text = str2 };
                        layout.Children.Add(label2);*/

                        //MainImage.ScaleTo(100.5);

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
    }

    public class Color
    {
        public int r, g, b, a;

        public Color(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 255;
        }

        public Color(int r, int g, int b, int a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        /// <summary>
        /// get the value of a pixel with alpha
        /// </summary>
        /// <returns>A float that represents the overall value of the pixel</returns>
        public float valueA()
        {
            return (r + g + b + a) / 4.0f;
        }

        /// <summary>
        /// get the value of a pixel without alpha
        /// </summary>
        /// <returns>A float that represents the overall value of the pixel</returns>
        public float value()
        {
            return (r + g + b) / 3.0f;
        }

        /// <summary>
        /// Convert an SKColor to a Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color SKColorToColor(SKColor color)
        {
            return new Color(color.Red, color.Green, color.Blue, color.Alpha);
        }

        /// <summary>
        /// Convert Color to an SKColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static SKColor ColorToSKColor(Color color)
        {
            return new SKColor((byte)color.r, (byte)color.g, (byte)color.b, (byte)color.a);
        }

        /// <summary>
        /// Takes a color reference and makes all of the values between 255 and 0
        /// </summary>
        /// <param name="color"></param>
        public static void toByte(ref Color color)
        {
            color.r = (color.r > 255) ? 255 : color.r ;
            color.r = (color.r < 0) ? 0 : color.r ;
            color.g = (color.g > 255) ? 255 : color.g ;
            color.g = (color.g < 0) ? 0 : color.g ;
            color.b = (color.b > 255) ? 255 : color.b;
            color.b = (color.b < 0) ? 0 : color.b;
            color.a = (color.a > 255) ? 255 : color.a;
            color.a = (color.a < 0) ? 0 : color.a;
        }

        public static void Add (ref int color, int i)
        {
            color += i;
        }

        public void Add(ref Color color)
        {
            r += color.r;
            g += color.g;
            b += color.b;
            a += color.a;
        }
        public void Subtract(ref Color color)
        {
            r -= color.r;
            g -= color.g;
            b -= color.b;
            a -= color.a;
        }

        public void SetColor(int r, int g, int b, int a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}