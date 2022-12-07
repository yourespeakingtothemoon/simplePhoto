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
        public LinkedList<Filter> filters = new LinkedList<Filter>();
        int pickleCount = 0;
      
     //Friend
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
                        saveButton.IsEnabled = true;
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
        public void applyFilters_Clicked(object sender, EventArgs e)
        {
            if (gSKB != null)
            {
                if (filters.Count <= 0)
                {
                    filterBuilder();
                }

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
            if (gSKB != null)
            {
              
                save.saveFile(bytes);
            }
            else 
            {
                saveButton.IsEnabled = false;
                saveButton.Text = "Upload Image First!";
            }
        }

        // building the preset filters
        private void filterBuilder()
        {
            switch (filterMenu.SelectedItem)
            {
                case "Pickle":
                    // filters.AddLast(new ValueAdj(-.5f));
                    if (pickleCount >= 8)
                    {

                        var stream = File.OpenRead("Assets/easteregg.jpg");// read the file as a stream
                        gSKB = SKBitmap.Decode(stream); // turn the stream into a SkiaSharp bitmap

                        SKData skd = gSKB.Encode(SKEncodedImageFormat.Png, 100);
                        Stream stream2 = new MemoryStream();
                        stream2 = skd.AsStream();
                        // display the image
                        imageView.Source = ImageSource.FromStream(() => stream2);
                        bytes = skd.ToArray();
                        pickleCount = 0;
                    }
                    else
                    {
                        filters.AddLast(new Add(new Color(-82, -52, -52)));
                        filters.AddLast(new Add(new Color(52, 82, 63)));
                        pickleCount++;
                    }
                   
                    break;
                case "Sea":
                    filters.AddLast(new Kuwahara(7));
                    filters.AddLast(new Grayscale());
                    filters.AddLast(new Lightness(.9f));
                    filters.AddLast(new Multiply(new Color(90,90,180)));


                    break;
                case "Lune":
                    /*filters.AddLast(new ValueAdj(.75f));
                    filters.AddLast(new Grayscale());*/
                    //filters.AddLast(new Interpolate(new Color(255, 50, 0), new Color(0, 50, 255)));
                    filters.AddLast(new LinearGradient(new Color(0, 50, 255),new Color(255, 50, 0)));
                    filters.AddLast(new Lightness(3f));
                    filters.AddLast(new LinearGradient(new Color(0, 50, 255),new Color(255, 50, 0),false));
                    break;
                case "Overblow":
                    filters.AddLast(new Bloom(90));
                    break;
                case "VirtualBoy":
                    filters.AddLast(new Bloom(150));
                    filters.AddLast(new Multiply(new Color(255, 0, 0)));
                    break;
                case "Film Negative":
                    //filters.AddLast(new Add(new Color(-50, -60, -10)));
                    filters.AddLast(new Grayscale());
                    filters.AddLast(new Add(new Color(-50, -60, -10)));
                    filters.AddLast(new Add(new Color(50, 100, 110)));
                    filters.AddLast(new ValueAdj(-1));
                    break;
                case "Painting":
                    filters.AddLast(new Kuwahara(7));
                    break;
              case "Magenta Storm":
                    filters.AddLast(new Bloom(200));
                    //filters.AddLast(new Multiply(new Color(255, 0, 0)));
                    Color mag1 = new Color(50, 200, 50);
                    Color mag2 = new Color(100,10,100);
                    //mag2.ToMultiplicitiveInverse();
                    //filters.AddLast(new Add(mag2));
                    
                    mag1.ToAdditiveInverse();
                    mag2.ToMultiplicitiveInverse();
                    filters.AddLast(new Add(mag1));
                    filters.AddLast(new Multiply(mag2));
                    break;


                case "Solar Sailer":
                    filters.AddLast(new Bloom(120));
                    filters.AddLast(new ValueAdj(.75f));
                    filters.AddLast(new Bloom(230));
                   // filters.AddLast(new Grayscale());
                    //filters.AddLast(new Multiply(new Color(255, 0, 0)));
                    Color red1 = new Color(255, 50, 0);
                    Color red2 = new Color(0, 50, 255);
                    //mag2.ToMultiplicitiveInverse();
                    //filters.AddLast(new Add(mag2));
                  
                    red1.ToAdditiveInverse();
                    red2.ToMultiplicitiveInverse();
                    filters.AddLast(new Add(red1));
                    filters.AddLast(new Multiply(red2));
                    filters.AddLast(new Add(new Color(0, -52, -52)));
                    filters.AddLast(new Add(new Color(200, 52, 52)));
                    filters.AddLast(new ValueAdj(.7f));
                    //filters.AddLast(new Add(new Color(-100, -100, -52)));
                    //filters.AddLast(new Add(new Color(52, 52, 100)));
                    break;
                case "'Merica":
               
                    filters.AddLast(new Bloom(230));
                    filters.AddLast(new Grayscale());
              
                    Color blue1 = new Color(255, 50, 0);
                    Color blue2 = new Color(0, 50, 255);
               
                   blue1.ToAdditiveInverse();
                
                    filters.AddLast(new Add(blue2));
                 
                    filters.AddLast(new Add(new Color(-52, -255, -200)));
                    filters.AddLast(new ValueAdj(1.5f));
                    break;
                case "Rowena":

                    filters.AddLast(new Bloom(200));
                    filters.AddLast(new Grayscale());

                    filters.AddLast(new Add(new Color(63, 100, 255)));
                  //  filters.AddLast(new Lightness(1.5f));
                    break;
                case "Boricha":
                    filters.AddLast(new Add(new Color(-65, -85, -85)));
                    break;
                case "Breaking Bad Mexico":
                    filters.AddLast(new Multiply(new Color(200, 175, 85)));
                    break;


                case "Did someone say Sepia Tone?":
                    filters.AddLast(new Grayscale());
                    //filters.AddLast(new ValueAdj(1.2f));
                    filters.AddLast(new Add(new Color(134, 86, 30)));
                    filters.AddLast(new ValueAdj(0.8f));
                    break;
                case "Custom":
                    Custom page = new Custom();  
                    Navigation.PushModalAsync(page);
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
