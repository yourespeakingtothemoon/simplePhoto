using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;

namespace PicturePickle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Custom : ContentPage
    {

        private Color color = new Color(255, 255, 255);
        //LinkedList<Filter> filters;
        int kernelSize = 7;
        int bloomThreshold = 200;
        string currentFilterString = "";
        //LinkedList<Filter> filtersPtr;

        public Custom()
        {
            InitializeComponent();

        }

        private void filterAddition(object sender, EventArgs e)
        {
            Color colorVal;
            Filter filter=null;
            Button btn = sender as Button;
            switch (btn.Text)
            {
                case "Add":   
                case "Subtract":
                case "Multiply":   
                case "Divide":


                    int Red, Green, Blue;
                    Red = 255;
                    Green = 255;
                    Blue = 255;
                    int Alpha = 255;
                        int.TryParse(usrR.Text, out Red);
                        int.TryParse(usrG.Text, out Green);
                        int.TryParse(usrB.Text, out Blue);
                        int.TryParse(usrA.Text, out Alpha);
                    colorVal = new Color(Red,Green,Blue,Alpha);

                    //colorVal.Set(new Color(int.PusrR.Text))
                    if (btn.Text == "Add") { filter = new Add(colorVal); }
                    if (btn.Text == "Subtract")
                    { colorVal.ToAdditiveInverse(); filter = new Add(colorVal); }
                    if (btn.Text == "Multiply")
                    { filter = new Multiply(colorVal); }
                    if (btn.Text == "Divide")
                    { colorVal.ToMultiplicitiveInverse(); filter = new Multiply(colorVal); }
                    break;
                    //layer 2
                case "Blur":
                    int blurAmt = 1;
                    int blurTimes = 10;
                    int.TryParse(usrBA.Text, out blurAmt);
                    int.TryParse(usrBT.Text, out blurTimes);
                    filter = new Blur(blurAmt, blurTimes);
                    break;
                case "Kuwahara":
                    int kernel = 7;
                    int.TryParse(usrK.Text, out kernel);
                    filter = new Kuwahara(kernel);
                    break;
                case "Bloom":
                    int threshold = 200;
                    int.TryParse(usrT.Text, out threshold);
                    filter = new Bloom(threshold);
                    break;
               //layer 3
                case "Adjust Value":
                    float val = -.5f;
                    float.TryParse(usrV.Text, out val);
                    filter = new ValueAdj(val);
                    break;
                    
                case "Adjust Lightness":
                    float val2 = .5f;
                    float.TryParse(usrL.Text, out val2);
                    filter = new Lightness(val2);
                    break;
                   
                case "Make Grayscale":
                    
                    filter = new Grayscale();
                    break;
                case "Invert Color":

                    filter = new Invert();
                    break;

            }

            if (filter != null)
            {
                App.main.filters.AddLast(filter);
                currentFilterString += filter.toString()+" | ";
            }

            filterLbl.Text = currentFilterString;

        }

        private void usrApply_Clicked(object sender, EventArgs e)
        {
           
            Navigation.PopModalAsync();
            App.main.applyFilters_Clicked(sender, e);
        }

        private void usrClear_Clicked(object sender, EventArgs e)
        {
            App.main.filters.Clear();
            currentFilterString = "";
            filterLbl.Text = currentFilterString;
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}