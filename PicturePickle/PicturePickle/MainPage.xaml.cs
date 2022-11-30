using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit;
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
            this.BindingContext = this;
        }
        // global things
        byte[] bytes;
        SKBitmap gSKB;
        LinkedList<Filter> filters = new LinkedList<Filter>();
      
        /*Add gAdd = new Add(new Color(105, 165, 126));
        Multiply gMult = new Multiply(new Color(255, 0, 0));
        Grayscale gGray = new Grayscale();
        Blur gBlur = new Blur(20, 5);
        Bloom gBloom = new Bloom();*/

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
                        //applyFilters.Text = "Apply Filter";
                        applyFilters.IsEnabled = true;
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
            if (gSKB != null)
            {
                filterBuilder();

                foreach (Filter filter in filters)
                {
                    filter.execute(ref gSKB);
                }

                filters.Clear();

                SKData skd = gSKB.Encode(SKEncodedImageFormat.Png, 100);
                Stream stream2 = new MemoryStream();
                stream2 = skd.AsStream();
                // display the image
                imageView.Source = ImageSource.FromStream(() => stream2);
                bytes = skd.ToArray();
            }
            else
            {
                applyFilters.IsEnabled = false;
                applyFilters.Text = "Upload Image First!";
                //applyFilters.
            }
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

        private void filterBuilder()
        {
            switch (filterMenu.SelectedItem)
            {
                case "Pickle":
                    //filters.AddLast(new ValueAdj(.7f));
                    filters.AddLast(new Add(new Color(105,165, 126)));
                   
                    break;
                case "Lune":
                    filters.AddLast(new ValueAdj(.75f));
                    filters.AddLast(new Grayscale());
                    break;
                case "Overblow":
                    filters.AddLast(new Bloom(90));
                    break;
                case "VirtualBoy":
                    filters.AddLast(new Bloom(150));
                    filters.AddLast(new Multiply(new Color(255, 0, 0)));
                    break;
                case "Custom":
                    Page1 page1 = new Page1();  
                    Navigation.PushModalAsync(page1);
                    break;
            }
        }

        private void filterMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterMenu.SelectedItem.Equals("Custom"))
            {
                applyFilters.Text = "Open Menu";
            }
            else
            {
                applyFilters.Text = "Apply "+filterMenu.SelectedItem;
            }
        }
    }
}
