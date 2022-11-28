using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using Xamarin.Essentials;
using System.Runtime.InteropServices.ComTypes;
using System.IO;

namespace PicturePickle
{
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Start the program
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

        }
        // global things
        byte[] bytes;
        SKBitmap gSKB;
        Add gAdd = new Add(new Color(105, 165, 126));
        Multiply gMult = new Multiply(new Color(255, 0, 0));
        Grayscale gGray = new Grayscale();

        // upload button event handler
        async private void uploadButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(); // have the user input a file
                if (result != null)
                {
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase)) // make sure that the file is a valid type
                    {
                        var stream = await result.OpenReadAsync(); // read the file as a stream
                        gSKB = SKBitmap.Decode(stream); // turn the stream into a SkiaSharp bitmap

                        SKData skd = gSKB.Encode(SKEncodedImageFormat.Png, 100);
                        Stream stream2 = new MemoryStream();
                        stream2 = skd.AsStream();
                        // display the image
                        imageView.Source = ImageSource.FromStream(()=>stream2);
                        bytes = skd.ToArray();
                    }
                }
                else
                {
                    //FileName.Text = "Incorrect file type. Please enter a .png or .jpg file.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // whatever we use to apply the filters
        // button / automatically when selected
        private void applyFilters_Clicked(object sender, EventArgs e)
        {
            gGray.execute(ref gSKB);
            SKData skd = gSKB.Encode(SKEncodedImageFormat.Png, 100);
            Stream stream2 = new MemoryStream();
            stream2 = skd.AsStream();
            // display the image
            imageView.Source = ImageSource.FromStream(() => stream2);
            bytes = skd.ToArray();
        }

        // save button event handler & things required for it to work
        public static ISave save { get; private set; }
        public static void Init(ISave saver) { MainPage.save = saver; }
        private void SaveImage_Clicked(object sender, EventArgs e)
        {
            try
            {
                save.saveFile(bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

      
    }
}
